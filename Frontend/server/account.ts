import axiosInstance from "@/lib/axios";

interface Account {
  userName: string;
  password: string;
}

export async function authenticate(account: Account) {
  try {
    const response = await axiosInstance.post("/account/authenticate", account);
    return response;
  } catch (error) {
    console.error("Authentication error:", error);
    throw error;
  }
}
