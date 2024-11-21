import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  transpilePackages: ["geist"],
  redirects: async () => {
    return [
      {
        source: "/",
        destination: "/dashboard",
        permanent: true
      }
    ];
  }
};

export default nextConfig;
