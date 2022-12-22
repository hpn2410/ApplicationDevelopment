namespace FPTBookStore.Migrations
{
    using FPTBookStore.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Antlr.Runtime.Tree;
    using System.Web.WebSockets;
    using System.Net.NetworkInformation;
    using System.Security.Policy;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "FPTBookStore.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                string[] roleList = { "Store Owner", "Admin" };

                CreateSeveralRoles(context, roleList);
                CreateSeveralUsers(context);
                CreateSeveralBooks(context);
            }
        }

        private void CreateSeveralRoles(ApplicationDbContext context, string[] roleList)
        {
            foreach (string role in roleList)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleResult = roleManager.Create(new IdentityRole(role));
                if (!roleResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", roleResult.Errors));
                }
            }
        }

        private void CreateSeveralUsers(ApplicationDbContext context)
        {
            CreateUser(context, "admin@gmail.com", "admin@gmail.com", "System Administrator", "Admin_123", "Admin");
            CreateUser(context, "nguyen@gmail.com", "nguyen@gmail.com", "Pnu", "123456", "Store Owner");
            CreateUser(context, "thien@gmail.com", "thien@gmail.com", "Thien", "123456", "Customer");
            CreateUser(context, "dang@gmail.com", "dang@gmail.com", "Dang", "123456", "Customer");
        }

        private void CreateUser(ApplicationDbContext context, string email, string userName, string fullName, string password, string role)
        {
            // create new user and set username, full name, email
            var user = new ApplicationUser()
            {
                UserName = userName,
                FullName = fullName,
                Email = email
            };

            // call user manager to hash the password and store the user in the database
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }

            // no need to link role to customer
            if (role == "Customer") return;

            // link role to user
            var addAdminRoleResult = userManager.AddToRole(user.Id, role);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreateSeveralBooks(ApplicationDbContext context)
        {
            // add categories
            context.Categories.Add(new Category()
            {
                Name = "Comic Book",
                Description = "A book intended to be consulted for information on specific matters rather than read from beginning to end.",
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            });

            context.Categories.Add(new Category()
            {
                Name = "Novel",
                Description = "A style of young adult novel primarily targeting high school and middle school students.",
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            });

            context.Categories.Add(new Category()
            {
                Name = "Textbook",
                Description = "A book that teaches a particular subject and that is used especially in schools and colleges.",
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            });

            context.SaveChanges();

            // add books
            context.Books.Add(new Book()
            {
                Name = "Attack On Titan",
                Author = "Isayama Hajime",
                Description = "Eren Jaeger lives in a city surrounded by stone walls. " +
                "The killer Titan is out there. For decades, members of the " +
                "Legion Reconnaissance Group were the only humans who dared to step outside " +
                "the walls and gather information about the Titans. Eren, a pacifist, " +
                "has no great desire to join them.",
                Category = context.Categories.First(c => c.Name == "Comic Book"),
                CoverUrl = "AOT.jpg",
                Price = 11.99M,
                StockedQuantity = 5000,
                CreatedDateTime = new DateTime(2013, 4, 7),
                UpdatedDateTime = new DateTime(2013, 4, 7)
            });

            context.Books.Add(new Book()
            {
                Name = "One Piece",
                Author = "Oda Eiichiro",
                Description = "One Piece is the story of a boy named Monkey D. Luffy, " +
                "who accidentally ate a Devil Fruit, turns into a rubber man and will never be able to" +
                " swim. Ten years after that incident, " +
                "he left his hometown and gathered 10 members to form a pirate crew, " +
                "nicknamed the Straw Hat Pirates. " +
                "That's when the One Piece treasure-hunting adventure begins. " +
                "In their adventure to find One Piece, the Straw Hat Pirates have to " +
                "fight many other bad pirates who also want to monopolize One Piece and " +
                "the Government Navy wants to eradicate the pirates. The Straw Hat Pirates " +
                "had to go through so many difficulties, not backing down with " +
                "the dream of Becoming the Pirate King and capturing the One Piece treasure.",
                Category = context.Categories.First(c => c.Name == "Comic Book"),
                CoverUrl = "OnePiece.jpg",
                Price = 10.99M,
                StockedQuantity = 38000,
                CreatedDateTime = new DateTime(1999, 10, 20),
                UpdatedDateTime = new DateTime(1999, 10, 20)
            });

            context.Books.Add(new Book()
            {
                Name = "Naruto",
                Author = "Masashi Kishimoto",
                Description = "Naruto is the story of Naruto Uzumaki, " +
                "a young ninja with dreams of becoming Hokage, " +
                "the leader of Konoha in order to seek recognition from everyone. " +
                "Twelve years ago, the nine-tailed fox attacked Konoha, " +
                "and the Fourth Hokage defeated and sealed the nine-tailed fox on his own son, " +
                "Naruto, to end this bloody attack. After that battle, " +
                "Naruto lost both his parents, he was shunned by everyone because " +
                "he carried a monster that killed so many people in Konoha. " +
                "After passing the graduation exam, Naruto and his two companions, " +
                "Sasuke Uchiha and Haruno Sakura, were led by Kakashi-sensei, " +
                "creating the famous Team 7, officially starting the adventure. " +
                "When watching Naruto, the audience will witness the daily growth of the boy " +
                "through episodes, battles with teachers and teammates.",
                Category = context.Categories.First(c => c.Name == "Comic Book"),
                CoverUrl = "Naruto.jpg",
                Price = 9.99M,
                StockedQuantity = 2200,
                CreatedDateTime = new DateTime(2007, 2, 15),
                UpdatedDateTime = new DateTime(2007, 2, 15)
            });

            context.Books.Add(new Book()
            {
                Name = "Doremon",
                Author = "Fujiko F. Fujio",
                Description = "The series tells about a cute blue robotic cat named Doraemon " +
                "from the 21st century, whose birthday is on September 3, 2112 and " +
                "becomes a close friend of boy Nobita, a very kind 4th grader. passed, was slow, " +
                "and never did well in school.",
                Category = context.Categories.First(c => c.Name == "Comic Book"),
                CoverUrl = "Doraemon.jpg",
                Price = 20M,
                StockedQuantity = 3600,
                CreatedDateTime = new DateTime(1992, 12, 11),
                UpdatedDateTime = new DateTime(1992, 12, 11)
            });

            context.Books.Add(new Book()
            {
                Name = "Colorful",
                Author = "Mori Eto",
                Description = "The content of the book is about a person " +
                "who commits a serious crime and dies without reincarnation. " +
                "But while this person's soul was losing its memory and floating aimlessly " +
                "to a dark place worthy of him, a white-winged angel appeared, raised his hand, " +
                "and announced that he had just won the lottery. the luck of heaven, " +
                "receiving the opportunity to practice and re-challenge. " +
                "If the practice goes well, the memories of the past life will gradually return. " +
                "The moment you remember the sin you have committed is also the end of " +
                "the boarding process. His soul will leave the borrowed body and return to heaven, " +
                "smoothly set foot in the cycle of reincarnation, reincarnated into another life.",
                Category = context.Categories.First(c => c.Name == "Novel"),
                CoverUrl = "Colorful.jpg",
                Price = 10M,
                StockedQuantity = 700,
                CreatedDateTime = new DateTime(2016, 3, 13),
                UpdatedDateTime = new DateTime(2016, 3, 13)
            });

            context.Books.Add(new Book()
            {
                Name = "Your Name",
                Author = "Shinkai Makoto",
                Description = "Mitsuha is a high school girl living in a remote countryside. " +
                "One day, she dreamed that she was in Tokyo in an unfamiliar room, " +
                "transformed into a boy, meeting friends she had never met. " +
                "Meanwhile, somewhere else, Taki, a Tokyo high school boy, " +
                "dreams of turning into a girl, living in the remote countryside. " +
                "Eventually the two of them realized they were being swapped with each other " +
                "through a dream. From the moment these two strangers met, " +
                "the wheel of fate began to move. This is the novel version of the animated film " +
                "Your Name., written by director Shinkai Makoto himself.",
                Category = context.Categories.First(c => c.Name == "Novel"),
                CoverUrl = "YourName.jpg",
                Price = 25M,
                StockedQuantity = 1800,
                CreatedDateTime = new DateTime(2016, 6, 18),
                UpdatedDateTime = new DateTime(2016, 6, 18)
            });

            context.Books.Add(new Book()
            {
                Name = "Byousoku 5 Centimeter",
                Author = "Makoto Shinkai",
                Description = "5 cm/s is the speed of falling cherry blossom petals, " +
                "which is also the average walking speed of a person. " +
                "The work is a series of short stories about two very attached people, " +
                "walking at the same speed, gradually heading in opposite directions. " +
                "An excellent work about emotional distance and a very mature ending.",
                Category = context.Categories.First(c => c.Name == "Novel"),
                CoverUrl = "PerSecond.jpg",
                Price = 10M,
                StockedQuantity = 3800,
                CreatedDateTime = new DateTime(1997, 11, 22),
                UpdatedDateTime = new DateTime(1997, 11, 22)
            });

            context.Books.Add(new Book()
            {
                Name = "Shigatsu Wa Kimi No Uso",
                Author = "Bagas_Ardiansyah",
                Description = "The story is about Arima Kosei, a piano prodigy. " +
                "But since the trauma caused by the death of his mother, " +
                "Kosei has not been able to hear any sound. Thought he would never touch the piano " +
                "keys again but that was before he met Miyazono Kawari...",
                Category = context.Categories.First(c => c.Name == "Novel"),
                CoverUrl = "April.jpg",
                Price = 10M,
                StockedQuantity = 3800,
                CreatedDateTime = new DateTime(1997, 11, 22),
                UpdatedDateTime = new DateTime(1997, 11, 22)
            });

            context.Books.Add(new Book()
            {
                Name = "The C++ Programming Language",
                Author = "Bjarne Stroustrup",
                Description = "With the new knowledge updated in this latest edition, " +
                "the author mainly updates new programming knowledge, introduces features, " +
                "libraries, and processing techniques according to C++11 standards. " +
                "considered a fairly advanced book, so if you have not mastered the basics, " +
                "you should refer to previous editions to keep up. This is a set of textbooks " +
                "that provide a full range of C++ programming knowledge and techniques with " +
                "instructions starting from the basics and being updated, enhanced, " +
                "and expanded with each edition.",
                Category = context.Categories.First(c => c.Name == "Textbook"),
                CoverUrl = "CPL.jpg",
                Price = 8.99M,
                StockedQuantity = 200,
                CreatedDateTime = new DateTime(2013, 2, 13),
                UpdatedDateTime = new DateTime(2013, 2, 13)
            });

            context.Books.Add(new Book()
            {
                Name = "Clean Code: A Handbook of Agile Software Craftsmanship",
                Author = "Robert C. Martin",
                Description = "Clean Code should be able to tell you the difference between " +
                "good code and bad code. You will know how to write good code and how to convert " +
                "bad code into good code.",
                Category = context.Categories.First(c => c.Name == "Textbook"),
                CoverUrl = "CleanCode.jpg",
                Price = 10.99M,
                StockedQuantity = 500,
                CreatedDateTime = new DateTime(2018, 2, 9),
                UpdatedDateTime = new DateTime(2018, 2, 9)
            });

            context.Books.Add(new Book()
            {
                Name = "The Pragmatic Programmer: From Journeyman to Master",
                Author = "Andrew Hunt",
                Description = "This book shows us how to become a better programmer in an " +
                "individual role or when working in a team or an organization. " +
                "This book is not theoretical but focuses on practical topics, helping us " +
                "to use our practical experience to make more informed decisions.",
                Category = context.Categories.First(c => c.Name == "Textbook"),
                CoverUrl = "TPP.jpg",
                Price = 6.99M,
                StockedQuantity = 180,
                CreatedDateTime = new DateTime(2011, 7, 3),
                UpdatedDateTime = new DateTime(2011, 7, 3)
            });

            context.Books.Add(new Book()
            {
                Name = "Code Complete",
                Author = "Steve McConnell",
                Description = "Here is a thorough expert look at the intricate process of " +
                "commercial software development. The text is rich in example code, " +
                "contains powerful insights on managing technical yet creative people, " +
                "and examines each milestone in software development in considerable detail. " +
                "Ideal for professional, self-taught, and student programmers.",
                Category = context.Categories.First(c => c.Name == "Textbook"),
                CoverUrl = "CC.jpg",
                Price = 7.99M,
                StockedQuantity = 250,
                CreatedDateTime = new DateTime(2013, 7, 29),
                UpdatedDateTime = new DateTime(2013, 7, 29)
            });
        }

    }
}
