using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Tag;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class TagBusiness : ITagBusiness
    {
        public CreateTagResponse CreateTag(CreateTagRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var tag = AutoMapperUtils.AutoMap<TagDTO, Tag>(request.Tag);
                        tag.Id = Guid.NewGuid();

                        _sqlContext.Tags.Add(tag);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new CreateTagResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new CreateTagResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };
                    }
                }
            }
        }
        public UpdateTagResponse UpdateTag(UpdateTagRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var tag = _sqlContext.Tags.FirstOrDefault(c => c.Id == request.Tag.Id);
                        if (tag == null) throw new Exception("Không tồn tại");
                        tag.Name = request.Tag.Name;

                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new UpdateTagResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new UpdateTagResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };
                    }
                }
            }
        }
        public DeleteTagResponse DeleteTag(DeleteTagRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                using (var _db = _sqlContext.Database.BeginTransaction())
                {
                    try
                    {
                        var tag = _sqlContext.Tags.FirstOrDefault(c => c.Id == request.Id);
                        if (tag == null) throw new Exception("Không tồn tại");

                        _sqlContext.Tags.Remove(tag);
                        _sqlContext.SaveChanges();
                        _db.Commit();
                        return new DeleteTagResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new DeleteTagResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };
                    }
                }
            }
        }
        public GetListTagResponse GetListTag(GetListTagRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var pagination = request.Pagination; 
                    var tags = _sqlContext.Tags.Where(c => 
                    (string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name))
                    ).OrderBy(c => c.Name);
                    pagination.Total = tags.Count();
                    var listTag = tags.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit);
                    var listTagDTOs = AutoMapperUtils.AutoMap<Tag, TagDTO>(listTag.ToList());

                    return new GetListTagResponse()
                    {
                        Code = 1,
                        Message = "Thành công",
                        Tags = listTagDTOs,
                        Pagination = pagination,
                    };
                }
                catch (Exception ex)
                {
                    return new GetListTagResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };
                }
            }
        }
        public GetTagByIdResponse GetTagById(GetTagByIdRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var tag = _sqlContext.Tags.FirstOrDefault(c => c.Id == request.Id);
                    if (tag == null) throw new Exception("Không tồn tại");

                    var tagDTO = AutoMapperUtils.AutoMap<Tag, TagDTO>(tag);

                    return new GetTagByIdResponse()
                    {
                        Code = 1,
                        Message = "Thành công",
                        Tag = tagDTO,
                    };
                }
                catch (Exception ex)
                {
                    return new GetTagByIdResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };
                }
            }
        }
        public GetAllTagResponse GetAllTag(GetAllTagRequest request)
        {
            using (var _sqlContext = new PnpWebContext())
            {
                try
                {
                    var tags = _sqlContext.Tags.OrderBy(obd => obd.Name).ToList();
                    var listTag = AutoMapperUtils.AutoMap<Tag, TagDTO>(tags);

                    return new GetAllTagResponse()
                    {
                        Code = 1,
                        Message = "Thành công",
                        Tags = listTag,
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllTagResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };
                }
            }
        }
    }
}
