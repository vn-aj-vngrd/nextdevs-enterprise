import { Client } from "@/lib/api-client";
import axiosInstance from "@/lib/axios";

export const client = new Client(
  process.env.NEXT_PUBLIC_API_URL,
  axiosInstance
);
