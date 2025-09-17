using library_system.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace library_system.Business
{
    public class ClientBO
    {

        private readonly AppDbContext _context;

        public ClientBO(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Client> getAllClients()
        {
            return getAllClients(false);
        }

        public IQueryable<Client> getAllClients(bool withHidden)
        {
            var clientsQuery = _context.Clients.AsQueryable();

            if (!withHidden) {
                //clientsQuery = clientsQuery.Where(client => client.isHidden != true);
            }

            return clientsQuery.OrderBy(client => client.FirstName).OrderBy(client => client.SecondName);
        } 

        public IQueryable<Client> getClientByID(int Id)
        {
            return _context.Clients.Where(client => client.Id == Id);
        }

        public bool CreateClient(Client client)
        {
            var t = Encrypt(client.Password);
            client.Password = t;

            try
            {
                if (client != null)
                {
                    _context.Clients.Add(client);   // Add employee to EF tracking
                    _context.SaveChanges();            // Commit to database
                    return true;  // Or any page you want
                }
                return false;

            }
            catch
            {
                // Log error if needed
                return false;
            }
        }

        public bool DeleteClient( int id)
        {

            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return false;
            }

            else
            {
                _context.Remove(client);
                _context.SaveChanges();
                return true;
            }

        }
        
        public bool updateClient(Client updated)
        {
            var Clients = _context.Clients.Find(updated.Id);

            if (Clients == null)
            {
                return false;
            }
            else
            {
                Clients.FirstName = updated.FirstName;
                Clients.SecondName = updated.SecondName;
                Clients.Username = updated.Username;
                Clients.Password=Encrypt(updated.Password);
                Clients.FiscalCode = updated.FiscalCode;
                Clients.Address = updated.Address;
                Clients.BadgeCode = updated.BadgeCode;

                _context.SaveChanges();
                
                return true;

            }

        }

        public static class Global
        {
            // set password
            public const string strPassword = "LetMeIn99$";

            // set permutations
            public const String strPermutation = "ouiveyxaqtd";
            public const Int32 bytePermutation1 = 0x19;
            public const Int32 bytePermutation2 = 0x59;
            public const Int32 bytePermutation3 = 0x17;
            public const Int32 bytePermutation4 = 0x41;
        }

        public static string Encrypt(string strData)
        {

            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strData)));
            // reference https://msdn.microsoft.com/en-us/library/ds4kkd55(v=vs.110).aspx

        }


        // decoding
        public static string Decrypt(string strData)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(strData)));
            // reference https://msdn.microsoft.com/en-us/library/system.convert.frombase64string(v=vs.110).aspx

        }

        // encrypt
        public static byte[] Encrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Global.strPermutation,
            new byte[] { Global.bytePermutation1,
                         Global.bytePermutation2,
                         Global.bytePermutation3,
                         Global.bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);


            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        // decrypt
        public static byte[] Decrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Global.strPermutation,
            new byte[] { Global.bytePermutation1,
                         Global.bytePermutation2,
                         Global.bytePermutation3,
                         Global.bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    }
}
