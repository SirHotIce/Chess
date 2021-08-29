using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using AE.Net.Mail;
using System.Windows.Forms;

namespace Chess
{
    class PopImap
    {
        SmtpClient gmail = new SmtpClient("smtp.gmail.com", 587);
        ImapClient ic;
        List<PictureBox> theBoardData = new List<PictureBox>();
        string _side;
        string email;
        string password;
        string recipentEmail;
        Mover mover;
        public PopImap(string side, List<PictureBox> theBoardData)
        {
            _side = side;
            this.theBoardData = theBoardData;
            if (_side.Equals("white"))
            {
                email = "dummyEmail1";
                password = "dummyEmailAppPassword";
                recipentEmail = "dummyEmai2";
                ic = new ImapClient("imap.gmail.com", email, password, AuthMethods.Login, 993, true);
                ic.NewMessage += new EventHandler<AE.Net.Mail.Imap.MessageEventArgs>(Recieve);
                Console.WriteLine(ic.IdleTimeout);

            }
            else if (_side.Equals("black"))
            {
                email = "dummyEmai2";
                password = "dummyEmailAppPassword";
                recipentEmail = "dummyEmail1";
                ic = new ImapClient("imap.gmail.com", email, password, AuthMethods.Login, 993, true);
                ic.NewMessage += new EventHandler<AE.Net.Mail.Imap.MessageEventArgs>(Recieve);
                Console.WriteLine(ic.IdleTimeout);

            }

        }

        public void Send(string move) 
        {
            try
            {
                gmail.EnableSsl = true;//encryting the connection
                gmail.DeliveryMethod = SmtpDeliveryMethod.Network;//delivering the smtp via the internet here it is called network
                gmail.UseDefaultCredentials = false;//this is false as it is only needed when we are using cloud storage and cloud comp stuff with our app
                gmail.Credentials = new NetworkCredential(email, password);

                System.Net.Mail.MailMessage theMessage = new System.Net.Mail.MailMessage();//creating the mail
                theMessage.Subject = "Move";
                theMessage.Body = move;
                theMessage.To.Add(recipentEmail);
                theMessage.From = new MailAddress(email);
                gmail.Send(theMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed...." + e);
            }
        }
        public void Recieve(object sender, AE.Net.Mail.Imap.MessageEventArgs e) {
            TheForm.isTurn = true;
            ic.SelectMailbox("INBOX");
            var recievedMail = ic.GetMessage(ic.GetMessageCount() - 1);
            Console.WriteLine("Opponent Moved "+recievedMail.Body.Trim());
            mover = new Mover(recievedMail.Body.Trim(), theBoardData);
            ic.DeleteMessage(recievedMail);
            Console.WriteLine("\nYour Turn.\n");            
        }
    }
}
