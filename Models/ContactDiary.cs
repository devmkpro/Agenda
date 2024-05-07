using System.Collections.Generic;
using System.IO;

namespace Agenda.Models
{
    public class ContactDiary
    {
        private List<Contact> _contacts = new List<Contact>();

        public void store(Contact contact)
        {
            _contacts.Add(contact);
        }

        public List<Contact> index()
        {
            return _contacts;
        }

        public void destroy(string name)
        {
            _contacts.RemoveAll(c => c.name == name);
        }

        public void ExportToCSV(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine("Nome, Telefone");

                foreach (Contact contact in _contacts)
                {
                    sw.WriteLine($"{contact.name}, {contact.phone}");
                }
            }
        }
    }
}
