import { Table, TableHead, TableRow, TableCell, TableBody, Paper } from '@mui/material'

export default function ProductList({ products }) {
  return (
    <Paper>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
            <TableCell>Colour</TableCell>
            <TableCell>Price</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {products.map(p => (
            <TableRow key={p.id}>
              <TableCell>{p.name}</TableCell>
              <TableCell>{p.colour}</TableCell>
              <TableCell>{p.price}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </Paper>
  )
}
