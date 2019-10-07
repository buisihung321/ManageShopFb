using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ManageShop.Domain;
using ManageShop.Server.Bussiness.Interface;
using ManageShop.Server.Repository.Infractructure;

namespace ManageShop.Server.Bussiness
{
    public class AlbumBusiness : IAlbumBusiness

    {

        private readonly AlbumRepository albumRepository;

        public AlbumBusiness()
        {
            albumRepository = new AlbumRepository();
        }

        public List<AlbumDomainModel> GetAlbums()
        {
            albumRepository.dbSet.Include(al)

            var albums = albumRepository.GetAll()
                .Select(a => new AlbumDomainModel
                {
                    Id = a.Id,
                    FbLink = a.FbLink,
                    Description = a.Description,
                    HasPosted = a.HasPosted,
                    CreatedTime = a.CreatedTime,
                    Name = a.Name,
                    PhotoCover =  a.PhotoCover,
                    AlbumId = a.AlbumId
                }).ToList();
            return albums;
        }

        public AlbumDomainModel GetAlbumById(int id)
        {
            throw new System.NotImplementedException();
        }

        public AlbumDomainModel AddOrEditAlbum()
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteAlbum(int id)
        {
            throw new System.NotImplementedException();
        }

        public AlbumDomainModel GetAlbumByCategoryName(string categoryName)
        {
            throw new System.NotImplementedException();
        }
    }
}
