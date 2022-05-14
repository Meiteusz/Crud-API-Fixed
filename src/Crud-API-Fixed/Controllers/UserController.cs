using AutoMapper;
using Controllers;
using Crud_API_Fixed.Models;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Crud_API_Fixed.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserController _userController;
        private readonly IMapper _mapper;

        public UserController(IUserController userController, IMapper mapper)
        {
            this._userController = userController;
            this._mapper = mapper;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUserConfirm(UserModel userModel)
        {
            try
            {
                var user = _mapper.Map<User>(userModel);
                var response = _userController.RegisterUser(user);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var response = _userController.GetUserById(id);
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var response = _userController.GetAllUsers();
                return Ok(response.Results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult Edit(int id)
        {
            var userModel = _mapper.Map<UserModel>(_userController.GetUserById(id).Data);
            return View(userModel);
        }

        public IActionResult EditUserConfirm(UserModel userModel)
        {
            try
            {
                var user = _mapper.Map<User>(userModel);
                var response = _userController.EditUser(user);
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var response = _userController.DeleteUser(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}