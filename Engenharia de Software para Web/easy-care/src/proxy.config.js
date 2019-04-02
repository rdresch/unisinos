const proxy = [
    {
      context: '/api',
      target: 'http://localhost:61141',
      pathRewrite: {'^/api' : ''}
    }
  ];
  module.exports = proxy;