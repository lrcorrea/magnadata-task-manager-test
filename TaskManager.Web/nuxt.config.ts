// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: "2025-07-15",
  devtools: { enabled: true },
  css: ["~/assets/styles/main.scss"],
  runtimeConfig: {
    public: {
      apiBaseUrl: "http://localhost:8080/api",
    },
  },
  app: {
    head: {
      link: [
        {
          rel: "stylesheet",
          href: "https://fonts.googleapis.com/css2?family=Kanit:wght@300;400;500;600&display=swap",
        },
        {
          rel: "icon",
          type: "image/x-icon",
          href: "/images/favicon.ico",
        },
        {
          rel: "icon",
          type: "image/png",
          href: "/images/favicon-32x32.png",
        },
      ],
    },
  },
});
