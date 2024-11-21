// Protecting routes with next-auth
// https://next-auth.js.org/configuration/nextjs#middleware
// https://nextjs.org/docs/app/building-your-application/routing/middleware

import NextAuth from "next-auth";
import authConfig from "./app/auth/auth.config";

const { auth } = NextAuth(authConfig);

export default auth((req) => {
  if (!req.auth && req.nextUrl.pathname !== "/auth/sign-in") {
    const newUrl = new URL("/auth/sign-in", req.nextUrl.origin);
    return Response.redirect(newUrl);
  }
});

export const config = {
  matcher: [
    /*
     * Match all request paths except for the ones starting with:
     * - api (API routes)
     * - _next/static (static files)
     * - _next/image (image optimization files)
     * - favicon.ico, sitemap.xml, robots.txt (metadata files)
     * - Static asset files (e.g., .svg, .png, .jpg, etc.)
     */
    "/((?!api|_next/static|_next/image|favicon.ico|sitemap.xml|robots.txt|.*\\.(?:svg|png|jpg|jpeg|gif|webp|ico|css|js|json|woff|woff2|ttf|eot|otf|mp4|webm|ogg|avi|mp3|wav|m4a|aac|oga)).*)"
  ]
};