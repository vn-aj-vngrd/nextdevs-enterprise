// Protecting routes with next-auth
// https://next-auth.js.org/configuration/nextjs#middleware
// https://nextjs.org/docs/app/building-your-application/routing/middleware

import NextAuth from "next-auth";
import authConfig from "./auth/auth.config";

const { auth } = NextAuth(authConfig);

export default auth((req) => {
  if (!req.auth && req.nextUrl.pathname !== "/auth/sign-in") {
    const newUrl = new URL("/auth/sign-in", req.nextUrl.origin);
    return Response.redirect(newUrl);
  }
});

export const config = { matcher: ["/dashboard/:path*"] };
