using System.Security.Claims;
using Core.DTOs;
using API.Extensions;
using API.Extenstions;
using API.Helpers;
using Core.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IAnalysisResultFileService _analysisResultFileService;

        private readonly IUnitOfWork _uow;
        public UsersController(
            IUnitOfWork uow,
            IMapper mapper,
            IPhotoService photoService,
            IAnalysisResultFileService analysisResultFileService
        )
        {
            _photoService = photoService;
            _analysisResultFileService = analysisResultFileService;
            _mapper = mapper;
            _uow=uow;
        }

       [HttpGet]
        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUser = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());
            userParams.CurrentUsername = currentUser.UserName;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = currentUser.Gender == "male" ? "female" : "male";
            }

            var users = await _uow.UserRepository.GetMembersAsync(userParams);

            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, 
                users.TotalCount, users.TotalPages));

            return Ok(users);
        }
        

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _uow.UserRepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null)
                return NotFound();

            _mapper.Map(memberUpdateDto, user);

            if (await _uow.Complete())
                return NoContent();

            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null)
                return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Photos.Count == 0)
                photo.IsMain = true;

            user.Photos.Add(photo);

            if (await _uow.Complete())
            {
                return CreatedAtAction(
                    nameof(GetUser),
                    new { username = user.UserName },
                    _mapper.Map<PhotoDto>(photo)
                );
            }

            return BadRequest("Problem adding photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null)
                return NotFound();

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null)
                return NotFound();

            if (photo.IsMain)
                return BadRequest("this is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null)
                currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _uow.Complete())
                return NoContent();

            return BadRequest("Problem setting the main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null)
                return NotFound();

            if (photo.IsMain)
                return BadRequest("You cannot delete your main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null)
                    return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await _uow.Complete())
                return Ok();

            return BadRequest("Problem deleting photo");
        }

        [HttpPost("add-analysisResultFile")]
        public async Task<ActionResult<AnalysisResultFileDTO>> AddAnalysisResultFile(
            Stream document
        )
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null)
                return NotFound();

            var result = await _analysisResultFileService.AddDocumentAsync(document);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var analysisResultFile = new AnalysisResultFile
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.AnalysisResultFiles.Count == 0)
                analysisResultFile.IsMainPDF = true;

            user.AnalysisResultFiles.Add(analysisResultFile);

            if (await _uow.Complete())
            {
                return CreatedAtAction(
                    nameof(GetUser),
                    new { username = user.UserName },
                    _mapper.Map<AnalysisResultFileDTO>(analysisResultFile)
                );
            }

            return BadRequest("Problem adding file");
        }

        [HttpDelete("delete-analysisResultFIle/{analysisResultFileId}")]
        public async Task<ActionResult> DeleteAnalysisResultFile(int analysisResultFileId)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            var analysisResultFile = user.AnalysisResultFiles.FirstOrDefault(
                x => x.Id == analysisResultFileId
            );

            if (analysisResultFile == null)
                return NotFound();

            if (analysisResultFile.IsMainPDF)
                return BadRequest("You cannot delete your main file");

            if (analysisResultFile.PublicId != null)
            {
                var result = await _analysisResultFileService.DeleteDocumentAsync(
                    analysisResultFile.PublicId
                );
                if (result.Error != null)
                    return BadRequest(result.Error.Message);
            }

            user.AnalysisResultFiles.Remove(analysisResultFile);

            if (await _uow.Complete())
                return Ok();

            return BadRequest("Problem deleting file");
        }
    }
}
