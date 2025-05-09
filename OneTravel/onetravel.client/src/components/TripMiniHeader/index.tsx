import React, { useState } from "react";
import styled from "styled-components";
import ITripModel from "../../models/ITripModel";
import { Link, useNavigate } from "react-router-dom";
import { Button } from "antd";
import { t } from "i18next";
import { CustomNavLinkSmall, Span } from "../Header/styles";


const Card = styled.div`
  border: 1px solid #d1d5db;
  border-radius: 16px;
  padding: 16px;
  background-color: white;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 1000px;

  cursor: pointer;
  transition: box-shadow 0.3s ease;
  overflow: hidden;

  &:hover {
    box-shadow: 0 4px 14px rgba(0, 0, 0, 0.15);
  }
`;


const Header = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

const Cities = styled.div`
  font-size: 1.125rem;
  font-weight: 600;
`;

const TimeSpan = styled.div`
  font-size: 0.875rem;
  color: #6b7280;
`;

const Details = styled.div<{ hovered: boolean }>`
  max-height: ${({ hovered }) => (hovered ? "500px" : "0")};
  overflow: hidden;
  transition: max-height 0.4s ease;
`;

const Section = styled.div`
  margin-top: 12px;
`;

const SectionTitle = styled.p`
  font-weight: 500;
  color: #374151;
`;



const TripMiniHeader: React.FC<ITripModel> = ({
  flight,
  hotel,
}) => {
  const [hovered, setHovered] = useState(false);
  const navigate = useNavigate()
  
  const onClickHandler = (value: string) => {
    navigate(`/${value}`)
  }

  return (
    <Card onMouseEnter={() => setHovered(true)} onMouseLeave={() => setHovered(false)}>
      <Header>
        <Cities>
          {flight.originCity} → {flight.destinationCity}
        </Cities>
        <TimeSpan>{flight.departing?.toDateString()} → {hotel.checkOutDate?.toDateString()}</TimeSpan>
        <TimeSpan>${flight.price + hotel.price}</TimeSpan>
        <Button onClick={() => onClickHandler("payment")}>{("Buy Now")}</Button>
      </Header>

      <Details hovered={hovered}>
        <Section>
          <SectionTitle>Flights: {flight.airline}</SectionTitle>
        </Section>
        <Section>
          <SectionTitle>Hotels: {hotel.name}</SectionTitle>
        </Section>
      </Details>
    </Card>
  );
};

export default TripMiniHeader;
