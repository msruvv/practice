using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailNetworkConsole2.Models
{
    internal class Seller
    {
        private DateTime _birthDate;

        public int SellerID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? Patronymic { get; set; }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                ValidateBirthDate(value);
                _birthDate = value;
            }
        }

        public bool IsActive { get; set; }

        public ContactInfo? ContactInfo { get; set; }
        public ICollection<ShopSeller> ShopSellers { get; set; } = new List<ShopSeller>();
        public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

        private void ValidateBirthDate(DateTime birthDate)
        {
            if (birthDate > DateTime.Now)
            {
                throw new ArgumentException("Дата рождения не может быть в будущем");
            }

            if (birthDate.Year < 1900)
            {
                throw new ArgumentException("Год рождения не может быть ранее 1900");
            }
        }

        public override string ToString()
        {
            return $"ID: {SellerID}\n" +
                   $"Фамилия: {LastName}\n" +
                   $"Имя: {FirstName}\n" +
                   $"Отчество: {Patronymic ?? "не указано"}\n" +
                   $"Дата рождения: {BirthDate:dd.MM.yyyy}\n" +
                   $"Активен: {(IsActive ? "Да" : "Нет")}";
        }
    }
}