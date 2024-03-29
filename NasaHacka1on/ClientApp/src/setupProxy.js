const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:26974';

const context = [
    //POST
    "/api/account/sign-in", // to jest legit login
    "/api/account/sign-up",
    "/api/account/forgot-password",

    //GET
    "/api/login", // to nie jest login 
    "/api/login/github",
    "/api/login/google",
    "/api/login/facebook",
];

const onError = (err, req, resp, target) => {
    console.error(`${err.message}`);
}

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    proxyTimeout: 10000,
    target: target,
    // Handle errors to prevent the proxy middleware from crashing when
    // the ASP NET Core webserver is unavailable
    onError: onError,
    secure: false,
    // Uncomment this line to add support for proxying websockets
    //ws: true, 
    headers: {
        Connection: 'Keep-Alive',
        origin: '*'
    }
  });

  app.use(appProxy);
};
