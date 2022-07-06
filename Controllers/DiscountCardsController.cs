using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProjectTestAPI_1.Models;
using ProjectTestAPI_1.SQLiteDb.Repository;

namespace ProjectTestAPI_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountCardsController
    {
        private readonly ILogger<UserController> _logger;
        private DiscountCardsRepository db;
        private VirtualCardRepository db1;
        public DiscountCardsController(ILogger<UserController> logger)
        {
            db = new DiscountCardsRepository();
            db1 = new VirtualCardRepository();
            _logger = logger;
        }
        [HttpPost]
        [Route("AddCard")]
        public IActionResult AddCard(
            [Required()]ulong cardNumber
            )
        {
            if(db.checkCardExist(cardNumber))
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestObjectResult(new Error(150, "Карта не найдена", "Кажется, что карта лояльности с указанным номером не найдена в системе. Пожалуйста, проверьте вводимые данные."
));
            }
        }
        [HttpPost]
        [Route("ConfirmCard")]
        public IActionResult ConfirmCard(
            [Required()]ulong cardNumber,
            [Required()]int code)
        {
            if(code == 1111)
            {
                if(db1.checkVirtualCardExist(cardNumber))
                {
                   return new BadRequestObjectResult(new Error(25,"Карта уже привязанна","Карта уже привязанна"));
                }
                else
                {
                return new OkObjectResult(db1.confirmVirtualCardDB(cardNumber));
                }
            }
            else
            {
                return new BadRequestObjectResult(new Error(24,"Неверный код подтверждения","Код подтверждения не подошел"));
            }
        }
        [HttpDelete]
        [Route("DeleteCard")]
        public IActionResult DeleteCard(
            [Required()]Guid cardToken)
        {
            db1.deleteVirtualCard(cardToken);
           return new OkResult();
        }

        [HttpGet]
        [Route("GetBalance")]
        public IActionResult GetBalance(Guid cardToken)
        {
            return new OkObjectResult(db.getBalance(db1.getVirtualCardNumber(cardToken)));
        }
    }
}