# TechLibrary

Aplicativo em C# que implementa uma biblioteca virtual. O usuário pode se registrar, autenticar, procurar livros no acervo e tomar emprestado os exemplares disponíveis.

> Fundamentos do C#, fundamentos do .NET, criação de usuários, autenticação de usuários, criptografia
de senhas com BCrypt, banco de dados, integração com banco de dados, implementação de tokens de acesso JWT,
definição e tratamento adequado de exceções personalizadas, paginação e filtros.

Desenvolvido no evento <b>NLW Connect - Csharp</b> (17&ndash;23/02/2025) da [Rocketseat](https://github.com/rocketseat)

## Uso
1.
```bash
git clone https://github.com/gp208/nlwConnectCs
```
2. Inicie o aplicativo no terminal com `dotnet run --project ./nlwConnectCs/TechLibrary.Api --launch-profile https` e abra https://localhost:7044/swagger/index.html no
navegador, ou execute o projeto pelo Visual Studio
3. Crie um usuário em <b>Users</b>
4. Entre com seu cadastro em <b>Login</b> e guarde o token de acesso
5. Clique em <b>Authorize</b> e digite 'Bearer' seguido de espaço e do token para obter o acesso
6. Procure os livros disponíveis em <b>Books</b> informando o número (1 a 3) da página da lista de livros ('pageNumber') ou parte do título
7. Forneça em <b>Checkouts</b> o ID do livro que deseja pegar emprestado
