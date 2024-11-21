import { NextAuthConfig } from "next-auth";
import CredentialProvider from "next-auth/providers/credentials";
import { AuthenticationRequest } from "../../lib/api-client";
import { client } from "../../lib/client";

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
          email: res.data!.email,
          accessToken: res.data!.jwToken
        };
      }
    })
  ],
  callbacks: {
    authorized({ auth, request: { nextUrl } }) {
      return !!auth;
    },
    async jwt({ token, user }) {
      if (user) {
        token.id = user.id;
        token.accessToken = user.accessToken;
      }

      return token;
    },
    async session({ session, token }) {
      if (token) {
        session.user.id = token.id as string;
        session.user.accessToken = token.accessToken as string;
      }

      return session;
    }
  },
  pages: {
    signIn: "auth/sign-in"
  }
} satisfies NextAuthConfig;

export default authConfig;
