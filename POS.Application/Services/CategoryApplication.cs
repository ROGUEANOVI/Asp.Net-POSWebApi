using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.DTOs.Request;
using POS.Application.DTOs.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Category;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly IMapper _mapper;
        private readonly CategoryValidator _validationRules;

        public CategoryApplication(IUnitOfWork uniOfWork, IMapper mapper, CategoryValidator validationRules)
        {
            _uniOfWork = uniOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<CategoryResponseDTO>>> ListCategories(BaseFilterRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CategoryResponseDTO>>();
            var categories = await _uniOfWork.CategoryRepository.ListCategories(filters);

            if (categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<CategoryResponseDTO>>(categories);
                response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESSFUL;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<CategorySelectResponseDTO>>> ListSelectCategories()
        {
            var response = new BaseResponse<IEnumerable<CategorySelectResponseDTO>>();
            var categories = await _uniOfWork.CategoryRepository.GetAllAsync();

            if (categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<CategorySelectResponseDTO>>(categories);
                response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESSFUL;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<CategoryResponseDTO>> CategoryById(int categoryId)
        {
            var response = new BaseResponse<CategoryResponseDTO>();
            var category = await _uniOfWork.CategoryRepository.GetByIdAsync(categoryId);

            if (category is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<CategoryResponseDTO>(category);
                response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESSFUL;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDTO requestDTO)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDTO);
            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;

                return response;
            }

            var category = _mapper.Map<Category>(requestDTO);
            response.Data = await _uniOfWork.CategoryRepository.RegisterAsync(category);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDTO requestDTO)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await CategoryById(categoryId);
            if (categoryEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var category = _mapper.Map<Category>(requestDTO);
            category.Id = categoryId;

            response.Data = await _uniOfWork.CategoryRepository.EditAsync(category);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveCategory(int categoryId)
        {
            var response = new BaseResponse<bool>();
            var category = await CategoryById(categoryId);
            if (category.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            response.Data = await _uniOfWork.CategoryRepository.RemoveAsync(categoryId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELELE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

    }
}
