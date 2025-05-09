import { lazy, useState } from "react";
import TravelHeaderContent from "./TravelHeaderContent.json";
import { Row, Col, Input, Button } from "antd";
import { Slide } from "react-awesome-reveal";
import { FormGroup } from "../../components/ContactForm/styles";
import TripResults from "../../components/TripResults"; // adjust path if needed
import {useSelector } from "react-redux";
import { fetchTripsAsync, selectTrips } from "../../redux/features/travel/travelSlice";
import { useAppDispatch } from "../../hooks";


const MiddleBlock = lazy(() => import("../../components/MiddleBlock"));
const Container = lazy(() => import("../../common/Container"));


const TravelSearch = () => {
  const dispatch = useAppDispatch();
  const [values, setValues] = useState({
    startCity: "",
    destinationCity: "",
    timeSpan: "",
  });
  const trips = useSelector(selectTrips); // Get trips from Redux state


  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setValues((prevValues) => ({ ...prevValues, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(fetchTripsAsync())

    // Add actual search logic or API call here
  };


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
                />
              </Col>
              <Col flex="1">
                <Input
                  type="text"
                  name="destinationCity"
                  placeholder="Destination City"
                  value={values.destinationCity || ""}
                  onChange={handleChange}
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
            </Row>
          </FormGroup>
          
        </Slide>

        <TripResults trips={trips} />

      </div>
    </Container >
  );
};

export default TravelSearch;
