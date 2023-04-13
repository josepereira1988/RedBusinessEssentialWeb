using AutoMapper;
using Entidades.Usuarios;
using Essencial.Interfaces.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedBusinessEssentialWeb.Models;
using X.PagedList;

namespace RedBusinessEssentialWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuarioService _service;
        private readonly IMapper _mapper;

        public UsuariosController(ILogger<UsuariosController> logger, IUsuarioService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int pagina = 1, string campo = "", string searchString = "")
        {
            try
            {
                var service = _service.dataset();
                X.PagedList.IPagedList<Usuarios> retorno = null;
                //string searchString1 = searchString == "" ? searchString : "";
                if (campo != "" && searchString != "")
                {
                    if (campo == "Nome")
                    {
                        retorno = await service.Where(i => i.Nome.Contains(searchString)).ToPagedListAsync(pagina, 10);
                    }
                    else
                    {
                        retorno = await service.Where(i => i.Usuario.Contains(searchString)).ToPagedListAsync(pagina, 10);
                    }
                }
                else
                {
                    retorno = await service.ToPagedListAsync(pagina, 10);
                }
                ViewBag.SearchString = searchString;
                ViewBag.Campo = campo;
                return View(retorno);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index(int pagina = 1,string campo = "",string searchString = "")
        //{
        //    try
        //    {
        //        string valor = ViewBag.valor1;
        //        if(searchString != "" && searchString != null)
        //        {
        //            ViewBag.SearchString = searchString;
        //            ViewBag.Campo = campo;

        //            if (campo == "Nome")
        //            {
        //                return View(await _service.dataset().Where(u => u.Nome.Contains(searchString)).ToPagedListAsync(pagina,10));
        //            }
        //            else
        //            {
        //                return View(await _service.dataset().Where(u => u.Usuario.Contains(searchString)).ToPagedListAsync(pagina, 10));
        //            }
        //        }
        //        else
        //        {
        //            return View(await _service.dataset().Where(u => u.Usuario.Contains(searchString)).ToPagedListAsync(pagina, 10));
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
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
        public async Task<IActionResult> Create(UsuariosCreateModel Musuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuarios usuario = new Usuarios();

                    await _service.Add(_mapper.Map<Usuarios>(Musuario));
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
        public async Task<IActionResult> Edit(int id, Usuarios usuarios)
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
