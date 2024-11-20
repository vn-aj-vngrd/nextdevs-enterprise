// lib/axios.js
import axios from "axios";

const axiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL, // Set the base URL from environment variables
  headers: {
    "Content-Type": "application/json"
  },
  timeout: 10000 // Timeout after 10 seconds
});

// You can add interceptors here if needed
axiosInstance.interceptors.request.use(
  (config) => {
    // Add token or other logic to headers before each request
    const token = localStorage.getItem("token");
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
    // Handle errors globally
    if (error.response && error.response.status === 401) {
      // Redirect to / on 401 Unauthorized
      window.location.href = "/";
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
