using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {

        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View(new UsuarioService().Listar());
        }

        public IActionResult RegistrarUsuario()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }

        [HttpPost]

        public IActionResult RegistrarUsuario(Usuario novoUsuario)
        {
            novoUsuario.Senha = Criptografo.TextoCriptografado(novoUsuario.Senha);
            new UsuarioService().incluirUsuario(novoUsuario);
            return RedirectToAction("CadastroRealizado");
        }

        public IActionResult CadastroRealizado()
        {
            return View();
        }

                public IActionResult editarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);
            return View(u);
        }

[HttpPost]
                 public IActionResult editarUsuario(Usuario userEditado)
        {
           UsuarioService us = new UsuarioService();
           us.editarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
            
        }

        public IActionResult ExcluirUsuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }

        [HttpPost]

        public IActionResult ExcluirUsuario(string decisao, int id)
        {
            if (decisao == "EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão de usuário" + new UsuarioService().Listar(id).Nome + "realizado com sucesso!";
                new UsuarioService().ExcluirUsuarios(id);
                return View("ListaDeUsuarios", new UsuarioService().Listar());

            }
            else
            {
                ViewData["Mensagem"] = "Exclusão Cancelada";
                return View("ListadeUsuarios", new UsuarioService().Listar());
            }
        }



        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index, Home");
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }
    }

}