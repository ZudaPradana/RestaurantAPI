using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.DTO.Request;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.ComponentModel;

namespace RestaurantAPI.Controllers
{
    [Route("api/foods")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodService _foodService;

        //inject service
        public FoodController(FoodService foodService)
        {
            this._foodService = foodService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Food>>> GetAllFood()
        {
            var response = await _foodService.GetAllFood();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFoodById(int id)
        {
            try
            {
                var findById = await _foodService.GetFoodById(id);
                if (findById == null)
                {
                    return NotFound($"Customer with ID {id} not found");
                }
                return Ok(findById);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Food>> AddFood([FromBody] FoodReqDTO reqDTO)
        {
            try
            {
                return new ObjectResult("Adding Successfully") { StatusCode = 201 };
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFood(int id)
        {
            try
            {
                await _foodService.GetFoodById(id);

                return Ok(new OkObjectResult($"Delete ID {id} Successfully"));
            } catch(Exception ex)
            {
                return BadRequest(ex.Message );
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditFood([FromBody] FoodReqDTO  reqDTO, int id)
        {
            try
            {
                await _foodService.UpdateFood(reqDTO, id);
                return new ObjectResult($"Edited ID {id} Successfully");
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
