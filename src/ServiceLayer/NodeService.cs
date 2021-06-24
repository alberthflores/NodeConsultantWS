using DomainLayer;
using DtoLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersistenceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface INodeService
    {
        Task<NodeValueDto> GetNodeById(int nodeId);
        Task<NodeValue2Dto> GetNodeWithChildrenById(int nodeId);
        Task<List<NodeDto>> GetAll();
    }
    public class NodeService:INodeService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<NodeService> logger;

        public NodeService(ApplicationDbContext dbContext, ILogger<NodeService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<List<NodeDto>> GetAll()
        {
            List<NodeDto> nodesDto = new List<NodeDto>();

            DateTime date = DateTime.Now;

            List<NodeValue> nodeValues = await dbContext.NodeValue.Include(n => n.Node).Where(n => n.CreatedAt <= date).ToListAsync();
            List<Node> nodes = await dbContext.Node.Include(n => n.Parent).ToListAsync();


            foreach (var item in nodes)
            {
                //Mapping node dto model.
                NodeDto nodeDto = new NodeDto();
                nodeDto.Id = item.Id;
                nodeDto.Description = item.Description;
                nodeDto.ParentId = item.ParentId;
                nodeDto.Value = nodeValues.Where(nv => nv.NodeId.Equals(item.Id)).Sum(nv=>nv.Value);
                nodeDto.TeenPercent = (nodeDto.Value * 0.1m);
                nodeDto.AccumulatedPercentageOfChildren = GetAccumulatedChildren(item, nodeValues, nodes) - (nodeDto.Value * 0.10m);
                nodesDto.Add(nodeDto);
            }

            return nodesDto;
        }

        public async Task<NodeValueDto> GetNodeById(int nodeId)
        {
            if (!(dbContext.Node.Any(n => n.Id.Equals(nodeId))))
            {
                throw new Exception($"Nodo con id:{nodeId} no encontrado.");
            }

            //Get values from database
            var node =await dbContext.Node.FindAsync(nodeId);
            var nodeValues = dbContext.NodeValue.Where(n => n.NodeId.Equals(nodeId));

            //Mapping result
            NodeValueDto nodeValueDto = new NodeValueDto();
            nodeValueDto.Id = nodeId;
            nodeValueDto.Description = node.Description;
            nodeValueDto.AccumulatedValue = Convert.ToDecimal(nodeValues.Sum(nv => nv.Value));

            return nodeValueDto;
        }

        public async Task<NodeValue2Dto> GetNodeWithChildrenById(int nodeId)
        {
            if (!(dbContext.Node.Any(n=>n.Id.Equals(nodeId))))
            {
                throw new Exception($"Nodo con id:{nodeId} no encontrado.");
            }

            DateTime date = DateTime.Now;

            List<NodeValue> nodeValues = await dbContext.NodeValue.Include(n => n.Node).Where(n => n.CreatedAt <= date).ToListAsync();
            List<Node> nodes = await dbContext.Node.Include(n => n.Parent).ThenInclude(n => n.Children).ToListAsync();

            var node = nodes.FirstOrDefault(n=>n.Id.Equals(nodeId));
            var nodeValue = nodeValues.Where(nv => nv.NodeId.Equals(node.Id)).Sum(nv => nv.Value);

            //Mapping node dto.
            NodeValue2Dto nodeDto = new NodeValue2Dto();
            nodeDto.Id = node.Id;
            nodeDto.Description = node.Description;
            nodeDto.Value = nodeValue;
            nodeDto.AccumulatedValueOfChildren = GetAccumulatedChildren(node, nodeValues, nodes) - (nodeValue * 0.10m);
            nodeDto.ToTheDatetime = date;

            return nodeDto;
        }

        private decimal GetAccumulatedChildren(Node nodeP, List<NodeValue> nodeValues, List<Node> nodes)
        {
            //Get value of the current node
            var nodeValue = nodeValues.Where(nv =>nv.NodeId.Equals(nodeP.Id)).Sum(nv => nv.Value);
            decimal acumulado = nodeValue * 0.10m;

            //Get children value
            foreach (var nodeItem in nodes.Where(n=>n.ParentId.Equals(nodeP.Id)))
            {
                acumulado =  GetAccumulatedChildren(nodeItem, nodeValues, nodes) + acumulado;
            }

            return acumulado;
            
        }
    }
}
