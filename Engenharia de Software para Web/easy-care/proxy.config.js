const proxy = [
    {
      context: '/api',
      target: 'http://localhost:61141/usuarionovo',
      pathRewrite: {'^/api' : ''}
    }
  ];
  module.exports = proxy;