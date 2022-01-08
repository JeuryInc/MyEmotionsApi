using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEmotions.Core.Entities;
using MyEmotions.Core.Interfaces.Repositories;
using MyEmotionsApi.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEmotionsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmotionController : ControllerBase
    {
        readonly IEmotionRepository _emotionRepository;
        private readonly ILogger _logger;
        readonly IMapper _mapper;

        public EmotionController(
            IEmotionRepository emotionRepository,
            ILogger logger,
            IMapper mapper
        )
        {
            _emotionRepository = emotionRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetEmotions")]
        public ActionResult<List<EmotionViewModel>> GetEmotions()
        {
            try
            {
                var emotions = _emotionRepository.AllIncluding(s => s.Owner).Where(x => x.IsPublic).OrderByDescending(x => x.CreationTime);

                return emotions.Select(_mapper.Map<EmotionViewModel>).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while getting the emotions: {ex}");
            }

            return new BadRequestResult();
        }

        [HttpGet("GetEmotionsByTag/{tag}")]
        public ActionResult<List<EmotionViewModel>> GetEmotionsByTag(string tag)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tag))
                    return new List<EmotionViewModel>();

                var emotions = _emotionRepository.AllIncluding(s => s.Owner).Where(x => x.Tags.Contains(tag));

                return emotions.Select(_mapper.Map<EmotionViewModel>).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while getting the emotions tags: {ex}");
            }

            return new BadRequestResult();
        }

        [HttpGet("GetTags")]
        public ActionResult<List<string>> GetTags()
        {
            try
            {
                var emotions = _emotionRepository.GetAll().Where(x => !string.IsNullOrEmpty(x.Content) && x.IsPublic).ToList();

                return GetTop20Tag(emotions).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while getting the top 20 tags: {ex}");
            }

            return new BadRequestResult();
        }

        private IEnumerable<string> GetTop20Tag(List<Emotion> emotions)
        {
            var tags = new List<string>();

            emotions.ForEach((e) =>
            {
                if (e.Tags != null)
                    foreach (var tag in e.Tags)
                        tags.Add(tag);
            });

            var groups = tags.GroupBy(v => v).OrderByDescending(x => x.Count()).Take(20).Select(x => x.Key);

            return groups;
        }

        [HttpGet("GetEmotionDetail/{id}")]
        public ActionResult<EmotionViewModel> GetEmotionDetail(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return new BadRequestResult();

                var emotion = _emotionRepository.GetSingle(s => s.Id == id, s => s.Owner);

                var userId = HttpContext.User.Identity.Name;

                return _mapper.Map<Emotion, EmotionViewModel>(
                    emotion
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while getting the top 20 tags: {ex}");
            }

            return new BadRequestResult();
        }

        [Authorize]
        [HttpGet("GetEmotionsByUser")]
        public ActionResult<List<EmotionViewModel>> GetEmotionsByUser()
        {
            string user = string.Empty;

            try
            {
                user = HttpContext.User.Identity.Name;

                if (string.IsNullOrEmpty(user))
                    return new BadRequestResult();

                var emotions = _emotionRepository.AllIncluding(s => s.Owner).Where(x => x.OwnerId == user).OrderByDescending(x => x.CreationTime).ToList();

                return emotions.Select(_mapper.Map<EmotionViewModel>).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while getting the emotions saved by user {user}: {ex}");
            }

            return new BadRequestResult();
        }

        [Authorize]
        [HttpPost("Create")]
        public ActionResult<string> Post([FromBody] EmotionViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ownerId = HttpContext.User.Identity.Name;
            var creationTime = DateTime.UtcNow;
            var emotionId = Guid.NewGuid().ToString();

            var emotion = new Emotion
            {
                Id = emotionId,
                Title = model.Title,
                Content = model.Content,
                Tags = model.Tags,
                IsPublic = model.IsPublic,
                CreationTime = creationTime,
                OwnerId = ownerId
            };

            _emotionRepository.Add(emotion);

            _emotionRepository.Commit();

            return Ok(new { id = emotionId });
        }
    }
}
