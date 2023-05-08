const PROXY_CONFIG = [
  {
    '/api/': {
      target: "https://localhost:7250",
      secure: false
    }
  }
]

module.exports = PROXY_CONFIG;
