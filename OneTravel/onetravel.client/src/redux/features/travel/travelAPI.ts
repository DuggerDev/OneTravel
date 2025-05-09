// A mock function to mimic making an async request for data

import ITripModel from "../../../models/ITripModel";
import { tripList } from "./tripMockData";

export function fetchTrips() {
    return new Promise<{ data: ITripModel[] }>((resolve) =>
      setTimeout(() => resolve({ data: tripList }), 750)
    );
  }