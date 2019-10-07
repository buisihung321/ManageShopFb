using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageShop.Domain;

namespace ManageShop.Server.Bussiness.Interface
{
    interface IAlbumBusiness
    {
        //CRUD OPERATIONS

        List<AlbumDomainModel> GetAlbums();
        AlbumDomainModel GetAlbumById(int id);
        AlbumDomainModel AddOrEditAlbum();
        bool DeleteAlbum(int id);

        AlbumDomainModel GetAlbumByCategoryName(string categoryName);
    }
}
