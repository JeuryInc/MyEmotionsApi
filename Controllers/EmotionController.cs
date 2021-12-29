using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEmotionsApi.API.ViewModels;
using MyEmotionsApi.Data.Interfaces;
using MyEmotionsApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEmotionsApi.Controllers
{ 
        [Route("api/[controller]")]
        [Authorize]
        [ApiController]
        public class EmotionController : ControllerBase
        {
            readonly IEmotionRepository _emotionRepository; 
            readonly IMapper _mapper;

            public EmotionController( 
                IEmotionRepository emotionRepository,
                IMapper mapper
            )
            {
                _emotionRepository = emotionRepository; 
                _mapper = mapper;
            }

            [HttpGet("GetEmotions")]
            public ActionResult<List<EmotionViewModel>> GetEmotions()
            {
                var emotions = _emotionRepository.AllIncluding(s => s.Owner);

                return emotions.Select(_mapper.Map<EmotionViewModel>).ToList();
            }

            [HttpGet("GetEmotionDetail/{id}")]
            public ActionResult<EmotionViewModel> GetEmotionDetail(string id)
            {
                var story = _emotionRepository.GetSingle(s => s.Id == id, s => s.Owner);
                var userId = HttpContext.User.Identity.Name;
                
                return _mapper.Map<Emotion, EmotionViewModel>(
                    story
                );
            }

            [HttpPost("Create")]
            public ActionResult<string> Post([FromBody] EmotionViewModel model)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var ownerId = HttpContext.User.Identity.Name;
                var creationTime = DateTime.UtcNow;
                var storyId = Guid.NewGuid().ToString();
                var story = new Emotion
                {
                    Id = storyId,
                    Title = model.Title,
                    Content = model.Content,
                    Tags = model.Tags,
                    CreationTime = creationTime, 
                    OwnerId = ownerId
                };

                _emotionRepository.Add(story);
                _emotionRepository.Commit();

                return storyId;
            } 

            [HttpDelete("{id}")]
            public ActionResult Delete(string id)
            {
                var ownerId = HttpContext.User.Identity.Name;
                if (!_emotionRepository.IsOwner(id, ownerId)) return Forbid("You are not the owner of this story");
                 
                _emotionRepository.DeleteWhere(story => story.Id == id);
                _emotionRepository.Commit();

                return NoContent();
        
        }
    }
}
