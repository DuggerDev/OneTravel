import { lazy, useEffect, useState } from "react";
import TravelHeaderContent from "./TravelHeaderContent.json";
import { Row, Col, Input, Button } from "antd";
import { Slide } from "react-awesome-reveal";
import { FormGroup } from "../../components/ContactForm/styles";
import TripResults from "../../components/TripResults"; // adjust path if needed
import { fetchTripsAsync, selectTrips } from "../../redux/features/travel/travelSlice";
import { useAppDispatch, useAppSelector } from "../../hooks";
import { fetchCitiesAsync, selectCities } from "../../redux/features/city/citySlice";


const MiddleBlock = lazy(() => import("../../components/MiddleBlock"));
const Container = lazy(() => import("../../common/Container"));


const TravelSearch = () => {

  const dispatch = useAppDispatch();
  const [values, setValues] = useState({
    startCity: "",
    destinationCity: "",
    timeSpan: "",
  });
  const trips = useAppSelector(selectTrips);
  const valid_cities = useAppSelector(selectCities);


  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setValues((prevValues) => ({ ...prevValues, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(fetchTripsAsync({
      originCity: values.startCity,
      destinationCity: values.destinationCity,
      startDate: new Date(values.timeSpan)
    }));
  };

  useEffect(() => {
    dispatch(fetchCitiesAsync())
  }, [dispatch]);

  return (
    <Container>
      <MiddleBlock
        title={TravelHeaderContent.title}
        content={TravelHeaderContent.text}
      />
      <div style={{ display: "flex", flexDirection: "column", alignItems: "center", gap: "2rem" }}>
        <Slide direction="right" triggerOnce>
          <FormGroup autoComplete="off" onSubmit={handleSubmit}>
            <Row gutter={[16, 16]} wrap={false} align="middle">
              <Col flex="1">
                <Input
                  type="text"
                  name="startCity"
                  placeholder="Start City"
                  value={values.startCity || ""}
                  onChange={handleChange}
                  list="city-list"
                />
              </Col>
              <Col flex="1">
                <Input
                  type="text"
                  name="destinationCity"
                  placeholder="Destination City"
                  value={values.destinationCity || ""}
                  onChange={handleChange}
                  list="city-list"
                />
              </Col>
              <Col flex="1">
                <Input
                  type="date"
                  name="timeSpan"
                  placeholder="Departure Date"
                  value={values.timeSpan || ""}
                  onChange={handleChange}

                />
              </Col>
              <Col flex="none">
                <Button htmlType="submit">{("Search")}</Button>
              </Col>
              <datalist id="city-list">
                {valid_cities.map((city, index) => (
                  <option key={index} value={city} />
                ))}
              </datalist>
            </Row>
          </FormGroup>

        </Slide>

        <TripResults trips={trips} />

      </div>
    </Container >
  );
};

export default TravelSearch;
