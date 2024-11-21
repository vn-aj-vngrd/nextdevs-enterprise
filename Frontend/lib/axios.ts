import axios from "axios";

const axiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  headers: {
    "Content-Type": "application/json"
  },
  timeout: 10000,
  transformResponse: (data) => data
});

axiosInstance.interceptors.request.use(
  (config) => {
    if (typeof window === "undefined") {
      return config;
    }

    const token = localStorage.getItem("accessToken");
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      if (typeof window !== "undefined") {
        localStorage.clear();

        // Delete all cookies
        deleteAllCookies();
      }
      window.location.href = "/auth/sign-in";
    }
    return Promise.reject(error);
  }
);

function deleteAllCookies(): void {
  // Get all cookies as a string
  const cookies = document.cookie.split(";");

  // Iterate through each cookie
  for (const cookie of cookies) {
    const cookieName = cookie.split("=")[0].trim();

    // Set the cookie with an expired date
    document.cookie = `${cookieName}=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/`;
  }
}

export default axiosInstance;
