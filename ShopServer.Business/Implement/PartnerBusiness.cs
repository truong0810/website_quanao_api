using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Partner;
using ShopServer.Business.Message.Product;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class PartnerBusiness : IPartnerBusiness
    {
        public CreatePartnerResponse CreatePartner(CreatePartnerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var partner = AutoMapperUtils.AutoMap<PartnerDTO, Partner>(request.Partner);
                        partner.Id = Guid.NewGuid();
                        partner.CreatedBy = request.AuthUserId;
                        partner.CreatedAt = DateTime.Now;

                        _sqlContext.Partners.Add(partner);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new CreatePartnerResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new CreatePartnerResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };
                    }
                }
            }
        }
        public UpdatePartnerResponse UpdatePartner(UpdatePartnerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var partner = _sqlContext.Partners.FirstOrDefault(c => c.Id == request.Partner.Id);
                        if (partner == null) throw new Exception("Không tồn tại");
                        var resource = _sqlContext.Resources.FirstOrDefault(c => c.Id == partner.ImageId);
                        if(resource != null)
                        {
                            _sqlContext.Resources.Remove(resource);
                        }
                        partner.Name = request.Partner.Name;
                        partner.NumberPhone = request.Partner.NumberPhone;
                        partner.Address = request.Partner.Address;
                        partner.Note = request.Partner.Note;
                        partner.ImageId = request.Partner.ImageId;
                        partner.UpdatedBy = request.AuthUserId;
                        partner.UpdatedAt = DateTime.Now;

                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new UpdatePartnerResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new UpdatePartnerResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };
                    }
                }
            }
        }
        public DeletePartnerResponse DeletePartner(DeletePartnerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var partner = _sqlContext.Partners.FirstOrDefault(c => c.Id == request.Id);
                        if (partner == null) throw new Exception("Không tồn tại");
                        var resource = _sqlContext.Resources.FirstOrDefault(c => c.Id == partner.ImageId);
                        if (resource != null)
                        {
                            _sqlContext.Resources.Remove(resource);
                        }
                        _sqlContext.Partners.Remove(partner);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new DeletePartnerResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new DeletePartnerResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };
                    }
                }
            }
        }
        public GetPartnerByIdResponse GetPartnerById(GetPartnerByIdRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var partner = _sqlContext.Partners.FirstOrDefault(c => c.Id == request.Id);
                    if (partner == null) throw new Exception("Không tồn tại");
                    var partnerDTO = AutoMapperUtils.AutoMap<Partner, PartnerDTO>(partner);
                    var resource = _sqlContext.Resources.FirstOrDefault(c => c.Id == partner.ImageId);
                    var resourceDTO = AutoMapperUtils.AutoMap<Resource, ResourceDTO>(resource);

                    return new GetPartnerByIdResponse()
                    {
                        Code = 1,
                        Message = "Thành công",
                        Resource = resourceDTO,
                        Partner = partnerDTO,
                    };
                }
                catch (Exception ex)
                {
                    return new GetPartnerByIdResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };
                }
            }
        }
        public GetListPartnerResponse GetListPartner(GetListPartnerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var pagination = request.Pagination;
                    var partners = _sqlContext.Partners.Where(c =>
                    (string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name))
                    && (string.IsNullOrEmpty(request.NumberPhone) || c.NumberPhone.Contains(request.NumberPhone))
                    && (string.IsNullOrEmpty(request.Address) || c.Address.Contains(request.Address))
                    ).OrderByDescending(obd => obd.CreatedAt);
                    pagination.Total = partners.Count();
                    var litsPartner = partners.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit).ToList();
                    var partnerDTO = AutoMapperUtils.AutoMap<Partner, PartnerDTO>(litsPartner);
                    partnerDTO.ForEach(item =>
                    {
                        var resource = _sqlContext.Resources.FirstOrDefault(c => c.Id == item.ImageId);
                        item.Resource = AutoMapperUtils.AutoMap<Resource, ResourceDTO>(resource);
                    });

                    return new GetListPartnerResponse()
                    {
                        Code = 1,
                        Message = "Thành công",
                        Pagination = pagination,
                        Partners = partnerDTO,
                    };
                }
                catch (Exception ex)
                {
                    return new GetListPartnerResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };
                }
            }
        }
        public GetAllPartnerResponse GetAllPartner(GetAllPartnerRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var partners = _sqlContext.Partners.OrderByDescending(c => c.CreatedAt);
                    var partnerDTOs = AutoMapperUtils.AutoMap<Partner, PartnerDTO>(partners.ToList());
                    return new GetAllPartnerResponse()
                    {
                        Code = 1,
                        Message = "Thành công",
                        Partners = partnerDTOs
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllPartnerResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };
                }
            }
        }
    }
}
