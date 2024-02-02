using MailKit.Net.Smtp;
using MimeKit;

namespace IntroEmailApplication
{
    internal class Program
    {
        private const int SmtpPort = 465;
        private const bool IsSslConnection = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the command line email client!");

            var user_wants_to_send_message = true;

            while (user_wants_to_send_message)
            {
                try //try to send an email, will catch any errors
                {
                    Console.WriteLine();
                    Console.WriteLine("New Message");

                    var mail = new MimeMessage();

                    mail.From.Add(new MailboxAddress("Dylan Kimmerling", "throw.dtk.away@gmail.com")); //Dylan throwaway email account

                    Console.Write("To: ");
                    mail.To.Add(new MailboxAddress("", Console.ReadLine()));
                    
                    Console.Write("Subject: ");
                    mail.Subject = Console.ReadLine();

                    Console.Write("Body: ");
                    mail.Body = new TextPart("plain") { Text = Console.ReadLine() };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", SmtpPort, IsSslConnection);
                        client.Authenticate("throw.dtk.away", "leiy yirs qufg qukv"); 
                        client.Send(mail);
                        client.Disconnect(true);

                        Console.WriteLine("Message sent successfully!");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("An error occurred, email not sent.");
                }

                Console.WriteLine();
                Console.Write("Would you like to send another email? (Y/N): ");

                if (Console.ReadKey().Key != ConsoleKey.Y)
                {
                    user_wants_to_send_message = false;
                }
            }
        }
    }
}