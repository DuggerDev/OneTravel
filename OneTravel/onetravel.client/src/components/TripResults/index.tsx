import React, { useMemo, useState } from "react";
import { Row, Col, Checkbox, Select, Button } from "antd";
import TripMiniHeader from "../../components/TripMiniHeader";
import ITripModel from "../../models/ITripModel";

interface TripResultsProps {
    trips: ITripModel[];
}

const TripResults: React.FC<TripResultsProps> = ({ trips }) => {
    const [selectedAirlines, setSelectedAirlines] = useState<string[]>([]);
    const [selectedHotels, setSelectedHotels] = useState<string[]>([]);
    const [selectedPriceRanges, setSelectedPriceRanges] = useState<string[]>([]);
    const [sortOrder, setSortOrder] = useState<"priceAsc" | "priceDesc" | "">("");

    const priceRanges = [
        { label: "Below $200", key: "below-200", test: (price: number) => price < 200 },
        { label: "$200 - $500", key: "200-500", test: (price: number) => price >= 200 && price <= 500 },
        { label: "$500 - $800", key: "500-800", test: (price: number) => price > 500 && price <= 800 },
        { label: "$800 - $1000", key: "800-1000", test: (price: number) => price > 800 && price <= 1000 },
        { label: "$1000+", key: "1000+", test: (price: number) => price > 1000 },
    ];

    const airlineCounts = useMemo(() => {
        const counts: Record<string, number> = {};
        trips.forEach((trip) => {
            const airline = trip.flight.airline;
            counts[airline] = (counts[airline] || 0) + 1;
        });
        return counts;
    }, [trips]);

    const hotelCounts = useMemo(() => {
        const counts: Record<string, number> = {};
        trips.forEach((trip) => {
            const hotel = trip.hotel.name;
            counts[hotel] = (counts[hotel] || 0) + 1;
        });
        return counts;
    }, [trips]);

    const filteredTrips = useMemo(() => {
        let filtered = [...trips];

        if (selectedAirlines.length) {
            filtered = filtered.filter((trip) => selectedAirlines.includes(trip.flight.airline));
        }

        if (selectedHotels.length) {
            filtered = filtered.filter((trip) => selectedHotels.includes(trip.hotel.name));
        }

        if (selectedPriceRanges.length) {
            filtered = filtered.filter((trip) => {
                const total = trip.flight.price + trip.hotel.price;
                return selectedPriceRanges.some((key) =>
                    priceRanges.find((range) => range.key === key)?.test(total)
                );
            });
        }

        if (sortOrder === "priceAsc") {
            filtered.sort((a, b) => (a.flight.price + a.hotel.price) - (b.flight.price + b.hotel.price));
        } else if (sortOrder === "priceDesc") {
            filtered.sort((a, b) => (b.flight.price + b.hotel.price) - (a.flight.price + a.hotel.price));
        }

        return filtered;
    }, [trips, selectedAirlines, selectedHotels, selectedPriceRanges, sortOrder]);

    const clearAllFilters = () => {
        setSelectedAirlines([]);
        setSelectedHotels([]);
        setSelectedPriceRanges([]);
        setSortOrder("");
    };

    return (
        <Row gutter={[24, 24]} style={{ width: "100%", paddingBottom: "4rem" }}>
            {/* Sidebar */}
            <Col xs={24} sm={8} md={6}>
                <div
                    style={{
                        background: "#f9f9f9",
                        padding: "1rem",
                        borderRadius: "8px",
                        boxShadow: "0 2px 8px rgba(0, 0, 0, 0.1)",
                        height: "100%",
                    }}
                >
                    <h1>Filters</h1>

                    <p>Sort by:</p>
                    <Select
                        value={sortOrder}
                        onChange={(value) => setSortOrder(value)}
                        style={{ width: "100%", marginBottom: "1rem" }}
                        options={[
                            { label: "None", value: "" },
                            { label: "Price (Low to High)", value: "priceAsc" },
                            { label: "Price (High to Low)", value: "priceDesc" },
                        ]}
                    />

                    <div style={{ marginBottom: "1rem" }}>

                        <div style={{ marginBottom: "1rem" }}>
                            <p>Price Range</p>
                            <Checkbox.Group
                                value={selectedPriceRanges}
                                onChange={(values) => setSelectedPriceRanges(values as string[])}
                            >
                                {priceRanges.map((range) => (
                                    <div key={range.key}>
                                        <Checkbox value={range.key}>{range.label}</Checkbox>
                                    </div>
                                ))}
                            </Checkbox.Group>
                        </div>

                        <p>Airlines</p>
                        <Checkbox.Group
                            value={selectedAirlines}
                            onChange={(values) => setSelectedAirlines(values as string[])}
                        >
                            {Object.entries(airlineCounts).map(([airline, count]) => (
                                <div key={airline}>
                                    <Checkbox value={airline}>{airline} ({count})</Checkbox>
                                </div>
                            ))}
                        </Checkbox.Group>
                    </div>

                    <div style={{ marginBottom: "1rem" }}>
                        <p>Hotels</p>
                        <Checkbox.Group
                            value={selectedHotels}
                            onChange={(values) => setSelectedHotels(values as string[])}
                        >
                            {Object.entries(hotelCounts).map(([hotel, count]) => (
                                <div key={hotel}>
                                    <Checkbox value={hotel}>{hotel} ({count})</Checkbox>
                                </div>
                            ))}
                        </Checkbox.Group>
                    </div>

                    <Button
                        onClick={clearAllFilters}
                        style={{ width: "100%", marginTop: "1rem" }}
                    >
                        Clear All Filters
                    </Button>
                </div>
            </Col>

            <Col xs={24} sm={16} md={18}>
                {filteredTrips.length > 0 ? (
                    <div style={{ display: "flex", flexDirection: "column", gap: "0.75rem", width: "100%" }}>
                        {filteredTrips.map((trip, idx) => (
                            <TripMiniHeader
                                key={idx}
                                flight={trip.flight}
                                hotel={trip.hotel}
                                totalPrice={trip.totalPrice}
                            />
                        ))}
                    </div>
                ) : (
                    <div style={{ padding: "2rem", textAlign: "center", color: "#6b7280" }}>
                        <h3>No results found</h3>
                        <p>Try broadening your search parameters.</p>
                    </div>
                )}
            </Col>
        </Row>
    );
};

export default TripResults;
