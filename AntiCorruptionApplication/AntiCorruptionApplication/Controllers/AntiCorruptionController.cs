using AntiCorruption.Business.Executor;
using AntiCorruption.Business.Interface;
using AntiCorruption.Model;
using AntiCorruption.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AntiCorruptionApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AntiCorruptionController : ControllerBase
    {
        private readonly ILogger<AntiCorruptionController> _logger;
        private readonly ICreateRepositoryExecutor _createRepositoryExecutor;
        private readonly IListBrachExecutor _listBranchExecutor;
        private readonly ICreateWebhooksExecutor _createWebhooksExecutor;
        private readonly IListWebHookExecutor _listWebHookExecutor;
        private readonly IUpdateWebHookExecutor _updateWebHookExecutor;

        public AntiCorruptionController(ILogger<AntiCorruptionController> logger, 
            ICreateRepositoryExecutor createRepositoryExecutor,
            IListBrachExecutor listBranchExecutor,
            ICreateWebhooksExecutor createWebhooksExecutor,
            IListWebHookExecutor listWebHookExecutor,
            IUpdateWebHookExecutor updateWebHookExecutor)
        {
            _logger = logger;
            _createRepositoryExecutor = createRepositoryExecutor;
            _listBranchExecutor = listBranchExecutor;
            _createWebhooksExecutor = createWebhooksExecutor;
            _listWebHookExecutor = listWebHookExecutor;
            _updateWebHookExecutor = updateWebHookExecutor;
        }

        [HttpPost(), Route("CreateRepository")]
        public ActionResult<long> CreateRepository(RepositoryModel repository)
        {
            try
            {
                return _createRepositoryExecutor.Execute(repository);
            }
            catch(DuplicatedRepositoryException e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpPost(), Route("CreateWebHook")]
        public ActionResult<RepositoryHookModel> CreateWebHook(long repositoryId, RepositoryHookModel hook, string repositoryName)
        {
            try
            {
                return Task.Run(async () => await _createWebhooksExecutor.Execute(repositoryId, hook, repositoryName)).GetAwaiter().GetResult();
            }
            catch(CreateHookException e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpGet(), Route("GetBrachsById")]
        public ActionResult<List<Branch>> GetBranch(long id)
        {
            try
            {
                return _listBranchExecutor.Execute(id);
            }
            catch(ListBrachException e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpGet(), Route("GetWebHook")]
        public ActionResult<List<RepositoryHookModel>> GetWebHook(string repositoryName)
        {
            try
            {
                return Task.Run(async () => await _listWebHookExecutor.Execute(repositoryName)).GetAwaiter().GetResult();
            }
            catch(ListWebHookException e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpPatch(), Route("UpdateWebHook")]
        public ActionResult<RepositoryHookModel> UpdateWebHook(RepositoryHookModel hook, string repositoryName)
        {
            try
            {
                return Task.Run(async () => await _updateWebHookExecutor.Execute(hook, repositoryName)).GetAwaiter().GetResult();
            }
            catch (UpdateWebHookException e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }
    }
}