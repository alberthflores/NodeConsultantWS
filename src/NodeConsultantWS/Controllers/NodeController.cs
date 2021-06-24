using DtoLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace NodeConsultantWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly INodeService nodeService;
        private readonly ILogger<NodeController> logger;
        public NodeController(INodeService nodeService, ILogger<NodeController> logger)
        {
            this.nodeService = nodeService;
            this.logger = logger;
        }

        // GET: api/
        [HttpGet]
        public async Task<ActionResult<ResponseApi<List<NodeDto>>>> GetAll()
        {
            ResponseApi<List<NodeDto>> result;
            try
            {
                result = new ResponseApi<List<NodeDto>>(await nodeService.GetAll());
                logger.LogInformation("Ok");
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Register log;
                result = new ResponseApi<List<NodeDto>>(new List<NodeDto>());
                result.StatusCode = 500;
                result.Message = ex.Message;
                logger.LogError(ex.Message);
                return result;
            }
        }

        // GET: api/GetNodeWithChildrenById
        [HttpGet("GetNodeWithChildrenById/{id}")]
        public async Task<ActionResult<ResponseApi<NodeValue2Dto>>> GetNodeWithChildrenById(int id)
        {
            ResponseApi<NodeValue2Dto> result;
            try
            {
                result = new ResponseApi<NodeValue2Dto>(await nodeService.GetNodeWithChildrenById(id));
                logger.LogInformation("Ok");
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Register log;
                result = new ResponseApi<NodeValue2Dto>(new NodeValue2Dto());
                result.StatusCode = 500;
                result.Message = ex.Message;
                logger.LogError(ex.Message);
                return result;
            }
        }

        // GET: api/GetNodeById/{id}
        [HttpGet("GetNodeById/{id}")]
        public async Task<ActionResult<ResponseApi<NodeValueDto>>> GetNodeById(int id)
        {
            ResponseApi<NodeValueDto> result;
            try
            {
                result = new ResponseApi<NodeValueDto>(await nodeService.GetNodeById(id));
                logger.LogInformation("Ok");
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Register log;
                result = new ResponseApi<NodeValueDto>(new NodeValueDto());
                result.StatusCode = 500;
                result.Message = ex.Message;
                logger.LogError(ex.Message);
                return result;
            }
        }
    }
}
