import axios from "axios";

const API = axios.create({
  baseURL: "http://localhost:5016/api",
});

export const getToken = async () => {
  const res = await API.post("/auth/token");
  return res.data.token;
};

export const setAuthToken = (token) => {
  if (token) {
    API.defaults.headers.common.Authorization = `Bearer ${token}`;
  } else {
    delete API.defaults.headers.common.Authorization;
  }
};

export const getProducts = async (colour) => {
  return API.get(`/products${colour ? `?colour=${colour}` : ""}`);
};

export const createProduct = async (data) => {
  return API.post("/products", data);
};
