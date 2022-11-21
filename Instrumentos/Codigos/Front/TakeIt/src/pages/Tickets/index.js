import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { ScrollView, StyleSheet, View, TouchableOpacity, Text, RefreshControl } from "react-native";
import { LoadingContext } from "../../context/LoadingContext";
import * as Animatable from 'react-native-animatable'
import { useNavigation } from "@react-navigation/native";

export default function Tickets() {
    const navigation = useNavigation();

    const {isLoading, setIsLoading} = useContext(LoadingContext);
    const [data, setData] = useState([]);

    const getTickets = async () => {
        try {
            setIsLoading(true);
            let responseTickets = await axios.get("/api/v1/tickets/owned");
            setData(responseTickets.data);
        } catch (err) {
            console.error(err);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        getTickets();
    }, []);

    return (
        <ScrollView style={{flex: 1}} refreshControl={
            <RefreshControl
                refreshing={isLoading}
                onRefresh={getTickets}
            />
        }>
            <View styles={styles.container}>
                {data.map((e, idx) =>
                <View key={idx} style={e.used ? styles.eventContainerOpacity : styles.eventContainer}>
                    <TouchableOpacity disabled={e.used} onPress={ () => navigation.navigate('Ticket', e) }>
                        <Animatable.Image
                            style={styles.image}  
                            source={{uri: e.event.imageUrl}} 
                            animation="fadeIn"
                            delay={300}
                            resizeMode="contain"
                        />
                    </TouchableOpacity>
                    <Text style={styles.date}>{new Date(e.ticket.startDate).toLocaleDateString('pt-BR')} {'>'} {new Date(e.ticket.endDate).toLocaleDateString('pt-BR')}</Text>
                    <Text style={styles.title}>{e.event.title}</Text>
                    <Text style={styles.qualification}>{e.ticket.name} ({e.ticket.qualification})</Text>
                    <Text style={styles.ticketType}>R${e.ticket.priceBrl}</Text>
                </View>
                )}
            </View>
        </ScrollView>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        // top: '5%',
        backgroundColor: '#ffff'
    },
    eventContainer: {
        marginBottom: 30
    },
    eventContainerOpacity: {
        marginBottom: 30,
        opacity: 0.3
    },
    imageContainer: {
        // top: '2%'
    },
    image: {
        height: 200,
        borderRadius: 15,
        border: 1,
        width: '90%',
        marginLeft: '5%',
    },
    textscontainer: {
        alignItems: 'center',
    },
    date: {
        fontSize: 20,
        // fontWeight: 'bold',
        color: '#7ED957',
        marginLeft: '6%',
        top: 3
    },
    title: {
        fontSize: 25,
        fontWeight: 'bold',
        marginLeft: '6%',
        top: 0
    },
    ticketType: {
        fontSize: 15,
        marginLeft: '6%',
        top: 0
    },
    qualification: {
        fontSize: 20,
        marginLeft: '6%',
        top: 0
    }
});