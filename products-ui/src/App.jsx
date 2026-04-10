import { useEffect, useState } from "react";
import { Container, Typography, Button, TextField, Stack } from "@mui/material";
import ProductForm from "./components/ProductForm";
import ProductList from "./components/ProductList";
import { getToken, setAuthToken, getProducts, createProduct } from "./api/api";

export default function App() {
  const [token, setToken] = useState("");
  const [products, setProducts] = useState([]);
  const [colour, setColour] = useState("");

  const load = async () => {
    const res = await getProducts(colour);
    setProducts(res.data);
  };

  const create = async (data) => {
    await createProduct(data);
    load();
  };

  useEffect(() => {
    const init = async () => {
      const t = await getToken();
      setToken(t);
      setAuthToken(t);
    };

    init();
  }, []);

  useEffect(() => {
    if (token) load();
  }, [token, colour]);

  return (
    <Container sx={{ mt: 4 }}>
      <Typography variant="h4">Products UI</Typography>

      <Stack direction="row" spacing={2} sx={{ my: 2 }}>
        <TextField
          label="Filter Colour"
          value={colour}
          onChange={(e) => setColour(e.target.value)}
        />
      </Stack>

      <ProductForm onCreate={create} />
      <ProductList products={products} />
    </Container>
  );
}
