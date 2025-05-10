// src/redux/tripSlice.ts
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { fetchCities } from "./cityAPI";
import { RootState } from "../../store";

interface CityState {
  data: string[];
  loading: boolean;
  error: string | null;
}

const initialState: CityState = {
  data: [],
  loading: false,
  error: null,
};

// Create an async thunk for fetching trips
export const fetchCitiesAsync = createAsyncThunk(
  'city/fetchCity',
  async () => {
    const response = await fetchCities();
    return response.data;
  }
);

const citySlice = createSlice({
  name: "city",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCitiesAsync.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCitiesAsync.fulfilled, (state, action) => {
        state.loading = false;
        state.data = action.payload;
      })
      .addCase(fetchCitiesAsync.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || "Something went wrong";
      });
  },
});

export const selectCities = (state: RootState) => state.city.data; // Selector to get trips


export default citySlice.reducer;
