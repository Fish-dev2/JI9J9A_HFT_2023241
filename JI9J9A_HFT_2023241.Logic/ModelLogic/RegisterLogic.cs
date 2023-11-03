using JI9J9A_HFT_2023241.Models;
using JI9J9A_HFT_2023241.Repository;
using JI9J9A_HFT_2023241.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Logic
{
    public class RegisterLogic
    {
        IRepository<Register> repository;

        public RegisterLogic(IRepository<Register> repository)
        {
            this.repository = repository;
        }

        public void Create(Register item)
        {
            //attribute ellenőrzés
            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            //id ellenőrzés
            this.repository.Delete(id);

        }

        public Register Read(int id)
        {
            var item = this.repository.Read(id);
            if (item == null)
            {
                throw new ArgumentException(item.GetType().Name + "not exists");
            }

            return item;
        }

        public IEnumerable<Register> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(Register item)
        {
            this.repository.Update(item);
        }

    }
}
