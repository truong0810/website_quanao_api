using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopServer.Business.DTO;
using ShopServer.Business.Inteface;
using ShopServer.Business.Message.Resource;
using ShopServer.Business.Ultis;
using ShopServer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class ResourceBusiness : IResourceBusiness
    {
        public readonly IConfiguration _configuration;
        public ResourceBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<UploadFileResponse> UploadFile(UploadFileRequest request)
        {
            using (var _context = new PnpWebContext())
            {
                using (var _dbTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        IFormFile file = request.File;
                        var fileId = Guid.NewGuid();
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fileExtension = fileName.Substring(fileName.LastIndexOf('.'));
                        var filePath = Path.Combine(fileName);
                        var pathToSave = Path.Combine(_configuration["resource-path"], filePath);
                        using (var bits = new FileStream(pathToSave, FileMode.Create))
                        {
                            await file.CopyToAsync(bits);
                        }
                        var resource = new Resource();
                        resource = new Resource()
                        {
                            Id = fileId,
                            FileName = fileName,
                            FilePath = pathToSave,
                            CreatedBy = request.AuthUserId,
                            CreatedDate = DateTime.Now,
                            Extension = fileExtension,
                        };
                        var resourceDTOs = AutoMapperUtils.AutoMap<Resource, ResourceDTO>(resource);
                        _context.Resources.Add(resource);
                        _context.SaveChanges();
                        _dbTransaction.Commit();
                        return new UploadFileResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                            Resource = resourceDTOs,
                        };
                    }
                    catch (Exception ex)
                    {
                        _dbTransaction.Rollback();
                        return new UploadFileResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };
                    }
                }
            }
        }
        public DeleteFileResponse DeleteFile(DeleteFileRequest request)
        {
            using (var _context = new PnpWebContext())
            {
                using (var _db = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var file = _context.Resources.FirstOrDefault(c => c.Id == request.Id);
                        var fullPath = file.FilePath;
                        if (File.Exists(fullPath)) { throw new Exception("Tệp này không tồn tại"); }
                        _context.Resources.Remove(file);
                        //switch (file.FileType)
                        //{
                        //    case "INTERNAL_NOTIFICATION":

                        //        break;
                        //    case "STAFF_ASSET":

                        //        break;
                        //    case "DOCUMENT":

                        //        break;
                        //}
                        _context.SaveChanges();
                        _db.Commit();
                        File.Delete(fullPath);
                        return new DeleteFileResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                        };
                    }
                    catch (Exception ex)
                    {
                        _db.Rollback();
                        return new DeleteFileResponse()
                        {
                            Code = 0,
                            Message = ex.Message,
                        };
                    }
                }
            }
        }

        public GetLinkImageResponse GetLink(string Id)
        {
            using (var _context = new PnpWebContext())
            {
                try
                {
                    var file = _context.Resources.FirstOrDefault(c => c.Id.ToString() == Id);
                    var filePath = "DEFAULT\\default-img.png";

                    if (file != null)
                    {
                        if (File.Exists(Path.Combine(_configuration["resource-path"], file.FileName)))
                            filePath = Path.Combine(_configuration["resource-path"], file.FileName);
                    }

                    using (var webClient = new WebClient())
                    {
                        byte[] imageByte = webClient.DownloadData(Path.Combine(_configuration["resource-path"], filePath));
                        return new GetLinkImageResponse()
                        {
                            Code = 1,
                            Message = "Thành công",
                            File = imageByte,
                        };
                    }

                }
                catch (Exception ex)
                {
                    return new GetLinkImageResponse()
                    {
                        Code = 0,
                        Message = ex.Message,
                    };
                }
            }
        }
        public DownLoadFileResponse DownLoadFile(DownLoadFileRequest request)
        {
            using (var _context = new PnpWebContext())
            {
                var resource = _context.Resources.FirstOrDefault(c => c.Id == request.Id);
                var resourceDTO = AutoMapperUtils.AutoMap<Resource, ResourceDTO>(resource);
                resourceDTO.FilePath = Path.Combine(_configuration["resource-path"], resource.FilePath);
                return new DownLoadFileResponse()
                {
                    Code = 1,
                    Message = "Thanh cong",
                    Resource = resourceDTO,
                };
            }
        }
    }
}
