using simpleBlog.Api.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simpleBlog.Api.Data.Seed
{
    public class InitSeedData
    {
        public static void Initialize(SimpleBlogContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            #region User
            var users = new User[]
            {
                  new User{Name="Reporter 1",Username="reporter1",Password="reporter1",IsActive=true,Email="reporter1@simpleblog.com",CreatedDate=DateTime.Now}
                  ,new User{Name="Reporter 2",Username="reporter2",Password="reporter2",IsActive=true,Email="reporter2@simpleblog.com",CreatedDate=DateTime.Now}
                  ,new User{Name="Reporter 3",Username="reporter3",Password="reporter3",IsActive=true,Email="reporter3@simpleblog.com",CreatedDate=DateTime.Now}
                  ,new User{Name="Reporter 4",Username="reporter4",Password="reporter4",IsActive=true,Email="reporter4@simpleblog.com",CreatedDate=DateTime.Now}
                  ,new User{Name="Reporter 5",Username="reporter5",Password="reporter5",IsActive=true,Email="reporter5@simpleblog.com",CreatedDate=DateTime.Now}
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
            #endregion

            #region Role

            var roles = new Role[]
            {
                new Role{RoleName="Admin",IsActive=true,CreatedDate=DateTime.Now}
                ,new Role{RoleName="Reporter",IsActive=true,CreatedDate=DateTime.Now}
            };
            foreach (Role r in roles)
            {
                context.Roles.Add(r);
            }
            context.SaveChanges();
            #endregion

            #region User Role

            var userRole = new UserRole[]
            {
                    new UserRole{UserId=1,RoleId=2}
                    ,new UserRole{UserId=2,RoleId=2}
                    ,new UserRole{UserId=3,RoleId=2}
                    ,new UserRole{UserId=4,RoleId=2}
                    ,new UserRole{UserId=5,RoleId=2}
            };
            foreach (UserRole ur in userRole)
            {
                context.UserRoles.Add(ur);
            }
            context.SaveChanges();
            #endregion

            #region Status
            var status = new Status[]
                {
                     new Status{Id=0,Nama="unpublish"}
                     ,new Status{Id=1,Nama="publish"}
                };
            foreach (Status s in status)
            {
                context.Statuses.Add(s);
            }
            context.SaveChanges();
            #endregion

            #region Artikel
            var artikel = new Artikel[]
            {
                new Artikel{ Title="Suatu Sore di Pelabuhan Sunda Kelapa", Content=@"Suatu sore yang terik pada pertengahan Februari 2019, sejumlah remaja naik ke atas kapal layar motor Sinar Keluarga yang bersandar di Pelabuhan Sunda Kelapa, Jakarta Utara. Sesampainya di anjong (segitiga penyeimbang) yang berada di bagian depan kapal, mereka bergantian melompat. Byur, byur, prakk suara tubuh bertemu dengan air laut, susul menyusul. Sementara buruh bongkar muat di sebelahnya terus bekerja, mengangkut muatan, memindahkan barang dari truk ke kapal.",PubDate=new DateTime(2020,01,02),AuthorId=2,Status=1, Excerpt= "Suatu sore yang terik pada pertengahan Februari 2019, sejumlah remaja naik ke atas kapal layar motor .....",CreatedDate=DateTime.Now}
                ,new Artikel{ Title="Gosip Merger Perusahaan Teknologi di Sekitar Kita", Content=@"Kabar rencana merger antara Gojek dan Grab serta Gojek dan Tokopedia menarik dicermati karena melibatkan dana yang sangat besar dan dapat mengubah dominasi bisnis berbasis daring.",PubDate=new DateTime(2020,01,04),AuthorId=1,Status=1, Excerpt= "Kabar rencana merger antara Gojek dan Grab serta Gojek dan Tokopedia menarik dicermati karena .....",CreatedDate=DateTime.Now}
            };
            foreach (Artikel s in artikel)
            {
                context.Artikels.Add(s);
            }
            context.SaveChanges();
            #endregion
        }
    }
}
