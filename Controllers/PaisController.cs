using AutoMapper;
using Entidades.Logadouro;
using Essencial.Interfaces.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace RedBusinessEssentialWeb.Controllers
{
    public class PaisController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IPaisService _service;
        private readonly IMapper _mapper;

        public PaisController(ILogger<UsuariosController> logger, IPaisService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int pagina = 1,string searchString = "")
        {
            try
            {
                var campo = ViewBag.SearchString;
                if (searchString != "" && searchString != null)
                {
                    ViewBag.SearchString = searchString;
                    var resultodo = await _service.dataset().Where(p => p.Nome.Contains(searchString)).ToPagedListAsync(pagina, 10);
                    return View(resultodo);
                }
                else
                {
                    ViewBag.SearchString = searchString;
                    var resultodo = await _service.dataset().ToPagedListAsync(pagina, 10);
                    return View(resultodo);
                }

                
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _service == null)
            {
                return NotFound();
            }

            var usuarios = await _service.GetById(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pais Musuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Pais usuario = new Pais();

                    await _service.Add(_mapper.Map<Pais>(Musuario));
                    return RedirectToAction(nameof(Index));
                }
                return View(Musuario);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return View(Musuario);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _service == null)
            {
                return NotFound();
            }

            var usuarios = await _service.GetById(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pais usuarios)
        {
            if (id != usuarios.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(usuarios);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.id).Result)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _service == null)
            {
                return NotFound();
            }

            var usuarios = await _service.GetById(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_service == null)
            {
                return Problem("Entity set 'MyContext.Usuarios'  is null.");
            }
            if (!UsuariosExists(id).Result)
            {
                _service.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }


        private async Task<bool> UsuariosExists(int id)
        {
            return await _service.ExistAsync(id);
        }
    }
}
