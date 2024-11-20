import { NextAuthConfig } from "next-auth";
import CredentialProvider from "next-auth/providers/credentials";
import { AuthenticationRequest } from "./lib/api-client";
import { client } from "./lib/client";

const protectedRoutes = [
  "/dashboard, /employees",
  "/orders",
  "/products",
  "/users",
  "/ai",
  "kanban"
];

const authConfig = {
  session: {
    strategy: "jwt"
  },
  providers: [
    CredentialProvider({
      credentials: {
        username: {
          type: "text"
        },
        password: {
          type: "password"
        }
      },
      async authorize(credentials) {
        if (!credentials.username || !credentials.password) {
          throw new Error("Missing credentials");
        }

        const body: AuthenticationRequest = {
          userName: credentials.username as string,
          password: credentials.password as string
        };

        const res = await client.authenticate(body);

        if (res.success === false) {
          return null;
        }

        return {
          id: res.data!.id,
          name: res.data!.userName,
          email: res.data!.email
        };
      }
    })
  ],
  callbacks: {
    authorized({ auth, request: { nextUrl } }) {
      const isLoggedIn = !!auth?.user;
      const isOnProtectedRoute = protectedRoutes.some(
        (route) => nextUrl.pathname === route
      );

      if (!isOnProtectedRoute) {
        if (isLoggedIn) return true;
        return false;
      } else if (isLoggedIn) {
        return Response.redirect(new URL("/dashboard", nextUrl));
      }

      return true;
    }
  },
  pages: {
    signIn: "/"
  }
} satisfies NextAuthConfig;

export default authConfig;
