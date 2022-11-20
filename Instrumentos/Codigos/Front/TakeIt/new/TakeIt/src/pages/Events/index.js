import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { View, StyleSheet, Text, TouchableOpacity } from "react-native";
import * as Animatable from 'react-native-animatable'
import { useNavigation } from "@react-navigation/native";
import Loading from '../../components/loading'
import { LoadingContext } from "../../context/LoadingContext";

export default function Events() {
    const navigation = useNavigation();

    const {setIsLoading} = useContext(LoadingContext);
    const [data, setData] = useState([]);

    const getEvents = async () => {
        try {
            setIsLoading(true);
            let events = await axios.get("/api/v1/events");
            setData(events.data);
        } catch (err) {
            console.error(err);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        getEvents();
    }, []);
    
    return (
        <View style={{flex: 1}}>
            <Loading />
            <View style={styles.container}>
                {data.map((e, idx) =>
                    <View style={styles.eventContainer}>
                        <TouchableOpacity onPress={ () => navigation.navigate('Event', e) }>
                            <Animatable.Image
                                style={styles.image} 
                                key={idx} 
                                source={{uri: e.imageUrl}} 
                                animation="fadeIn"
                                delay={300}
                                resizeMode="contain"
                            />
                        </TouchableOpacity>
                        <Text style={styles.date}>{new Date(e.startDate).toLocaleDateString('pt-BR')} {'>'} {new Date(e.endDate).toLocaleDateString('pt-BR')}</Text>
                        <Text style={styles.title}>{e.title}</Text>
                        <Text style={styles.location}>{e.location}</Text>
                    </View>
                )}
            </View>
        </View>
        
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#ffff'
    },
    eventContainer: {
        top: '2%',
        marginBottom: 50,
        // justifyContent: 'center',
        // alignItems: 'center'
    },
    image: {
        height: 200,
        borderRadius: 15,
        border: 1,
        width: '90%',
        marginLeft: '5%',
        top: '2%'
    },
    date: {
        fontSize: 15,
        // fontWeight: 'bold',
        color: '#7ED957',
        marginLeft: '6%',
        top: 20
    },
    title: {
        fontSize: 20,
        fontWeight: 'bold',
        marginLeft: '6%',
        top: 20
    },
    location: {
        fontSize: 12,
        marginLeft: '6%',
        top: 19
    }
});