using System.Transactions;
using BankAccount.Data;
using BankAccount.Migrations;
using BankAccount.Models;
using BankAccount.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private AppDbContext db;
        private UserManager<ApplicationUser> userManager;
        public TransactionsController(AppDbContext _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
        public async Task<IActionResult> ViewTransaction(SearchViewModel model)
        {

            var userId = userManager.GetUserId(User); //we get the user's id from db
            if (userId == null || string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }//if id removed from url user will be redirected to home index 
            var FindTrans = await db.MyTransactions.Where(x => x.UserId == userId).ToListAsync();
            if (model.Amount > 0)
            {
                FindTrans = FindTrans.Where(trans => trans.Amount == model.Amount).ToList();
            }

            if (model.FromDate != DateTime.MinValue && model.ToDate != DateTime.MinValue)
            {
                FindTrans = FindTrans.Where(trans => trans.DateOfTransaction.Date >= model.FromDate && trans.DateOfTransaction.Date <= model.ToDate).ToList();
            }

            if (model.Type != null && !model.Type.Equals("All"))
            {
                FindTrans = FindTrans.Where(trans => trans.Transaction.ToString().Equals(model.Type)).ToList();

            }
            var search = new SearchViewModel()
            {
                Transactions = FindTrans,
            };
            return View(search);
        }
        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            var CurrentUser = await userManager.GetUserAsync(User);

            var users = await userManager.Users.Where(c => c.Id != CurrentUser!.Id).ToListAsync();
            ViewBag.MyBalance = CurrentUser.Balance;
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Transfer(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            AllTransactionsViewModel model = new AllTransactionsViewModel()
            {

                Balance = user.Balance,
                UserId = user.Id,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Transfer(AllTransactionsViewModel model)
        {
            var user = await userManager.GetUserAsync(User);

            var receiver = await userManager.FindByIdAsync(model.UserId);
            if (receiver == null)
            {
                ModelState.AddModelError("", "The selected receiver does not exist.");
                return View(model);
            }


            if (model.TypeOT == MyTransaction.Transactions.Transfer)
            {
                if (user.Balance >= model.Amount)
                {

                    user.Balance = user.Balance - model.Amount;
                    await userManager.UpdateAsync(user);


                    receiver.Balance = receiver.Balance + model.Amount;
                    await userManager.UpdateAsync(receiver);


                    MyTransaction userTransfer = new MyTransaction()
                    {
                        Amount = model.Amount,
                        DateOfTransaction = DateTime.Now,
                        NewAmount = user.Balance,
                        Transaction = model.TypeOT,
                        UserId = user.Id,
                        MyTransactionId = Guid.NewGuid().ToString()
                    };
                    db.MyTransactions.Add(userTransfer);
                    await db.SaveChangesAsync();

                    MyTransaction receiverTransaction = new MyTransaction()
                    {
                        Amount = model.Amount,
                        DateOfTransaction = DateTime.Now,
                        NewAmount = receiver.Balance,
                        Transaction = MyTransaction.Transactions.Deposit,
                        UserId = receiver.Id,
                        MyTransactionId = Guid.NewGuid().ToString()
                    };
                    db.MyTransactions.Add(receiverTransaction);


                    await db.SaveChangesAsync();

                    return RedirectToAction("UsersList");
                }
                else
                {
                    return RedirectToAction(nameof(InsufficientFunds));
                }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Operations()
        {
            var user = await userManager.GetUserAsync(User);
            AllTransactionsViewModel model = new AllTransactionsViewModel()
            {

                Balance = user.Balance,
                UserId = user.Id,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Operations(AllTransactionsViewModel model)
        {
            var user = await userManager.GetUserAsync(User);

            if (model.TypeOT == (MyTransaction.Transactions)1)
            {
                user.Balance = user.Balance + model.Amount;
                await userManager.UpdateAsync(user);

                MyTransaction objTrana = new MyTransaction()

                {
                    Amount = model.Amount,
                    DateOfTransaction = DateTime.Now,
                    NewAmount = user.Balance,
                    Transaction = model.TypeOT,
                    UserId = user.Id,
                    MyTransactionId = Guid.NewGuid().ToString()
                };
                db.MyTransactions.Add(objTrana);
                db.SaveChanges();
                return RedirectToAction("ViewProfile", "Account");
            }
            else if (model.TypeOT == (MyTransaction.Transactions)2) //withdraw
            {
                if (user.Balance >= model.Amount)
                {
                    user.Balance = user.Balance - model.Amount;
                    await userManager.UpdateAsync(user);
                }
                else
                {
                    return RedirectToAction(nameof(InsufficientFunds));
                }
                MyTransaction objTrana = new MyTransaction()
                {
                    Amount = model.Amount,
                    DateOfTransaction = DateTime.Now,
                    NewAmount = user.Balance,
                    Transaction = model.TypeOT,
                    UserId = user.Id,
                    MyTransactionId = Guid.NewGuid().ToString()

                };
                db.MyTransactions.Add(objTrana);
                db.SaveChanges();
                return RedirectToAction("ViewProfile", "Account");
            }

            db.SaveChanges();
            return RedirectToAction("ViewProfile", "Account");
        }
        public IActionResult InsufficientFunds()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            if (model == null)
            {
                return View(nameof(DoesntExist));
            }
            var transactions = db.MyTransactions.ToList();
            if (!string.IsNullOrEmpty(model.SearchString))
            {
                var result = transactions.Where(s => s.Amount == Convert.ToDecimal(model.SearchString));
            }
            return View();
        }
        public IActionResult DoesntExist()
        {
            return View();
        }

    }
}
