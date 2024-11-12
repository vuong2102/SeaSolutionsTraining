using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSTraining.Share
{
    public class Helper
    {
        private readonly ApplicationDbContext _context;

        public Helper(ApplicationDbContext context)
        {
            _context = context;
        }
        public string GetNextOrderCode()
        {
            try
            {
                var lastOrder = _context.Order.OrderByDescending(o => o.OrderCode).FirstOrDefault();
                int lastNumber = 0;
                if (lastOrder != null)
                {
                    string numericPart = Regex.Match(lastOrder.OrderCode, @"\d+").Value;
                    lastNumber = int.Parse(numericPart);
                }
                lastNumber++;

                string formattedNumber = lastNumber.ToString("D4");

                return $"ORD{formattedNumber}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

    }
}
