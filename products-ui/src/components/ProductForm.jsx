import { useState } from "react";
import { TextField, Button, Paper, Stack } from "@mui/material";

export default function ProductForm({ onCreate }) {
  const [form, setForm] = useState({ name: "", colour: "", price: "" });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async () => {
    await onCreate({ ...form, price: parseFloat(form.price) });
    setForm({ name: "", colour: "", price: "" });
  };

  return (
    <Paper sx={{ p: 2, mb: 2 }}>
      <Stack spacing={2}>
        <TextField
          label="Name"
          name="name"
          value={form.name}
          onChange={handleChange}
        />
        <TextField
          label="Colour"
          name="colour"
          value={form.colour}
          onChange={handleChange}
        />
        <TextField
          label="Price"
          name="price"
          type="number"
          value={form.price}
          onChange={handleChange}
        />
        <Button variant="contained" onClick={handleSubmit}>
          Add
        </Button>
      </Stack>
    </Paper>
  );
}
