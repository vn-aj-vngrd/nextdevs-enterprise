import axiosInstance from "@/lib/axios";

interface Account {
  userName: string;
  password: string;
}

export async function authenticate(account: Account) {
  try {
    const response = await axiosInstance.post("/api/authenticate", account);
    return response.data; // Return only the data if needed
  } catch (error) {
    console.error("Authentication error:", error);
    throw error; // Re-throw for handling elsewhere
  }
}
