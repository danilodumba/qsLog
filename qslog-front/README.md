# Como foi desenvolvido o frontend?

O frontend do qs.log foi desenvolvido com Angular na sua versão 11. 

Foi utilizado um template bootstrap na versão 4.

# Como instalar?

1. Rode o npm.

```javascript
    npm install
``` 

2. Atualize o arquivo `environment.ts` para apontar para sua API.

```javascript
    export const environment = {
    production: true,
    api: '[sua-api]'
};
``` 

3. Gere o build do front.

Rode `ng build --prod` para gerar o build na pasta  `dist/`

