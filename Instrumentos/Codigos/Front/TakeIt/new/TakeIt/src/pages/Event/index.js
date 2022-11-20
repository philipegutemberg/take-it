import { useNavigation } from "@react-navigation/native";
import axios from "axios";
import { useContext } from "react";
import { View, StyleSheet, Text, TouchableOpacity } from "react-native";
import * as Animatable from 'react-native-animatable'
import Loading from '../../components/loading'
import { LoadingContext } from "../../context/LoadingContext";

export default function Event({route}) {
    const navigation = useNavigation();
    const item = route.params;
    const {setIsLoading} = useContext(LoadingContext);

    const buy = async (callBack) => {
        try {
            setIsLoading(true);
            await axios.post("/api/v1/events/buy", {
                EventTicketTypeCode: item.ticketTypes[0].code
            });
            if (callBack)
                callBack();
        } catch (err) {
            console.error(err)
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <View style={styles.container}>
            <Loading />
            <View style={styles.eventContainer}>
                <View style={styles.imageContainer}>
                    <Animatable.Image 
                        style={styles.image}
                        source={{uri: item.imageUrl}} 
                        animation="fadeIn"
                        delay={300}
                        resizeMode="contain"
                    />
                </View>
                <View style={styles.textscontainer}>
                    <Text style={styles.date}>{new Date(item.startDate).toLocaleDateString('pt-BR')} {'>'} {new Date(item.endDate).toLocaleDateString('pt-BR')}</Text>
                    <Text style={styles.title}>{item.title}</Text>
                    <Text style={styles.location}>{item.location}</Text>
                    <Text style={styles.description}>{item.description}</Text>
                </View>
            </View>

            <View style={styles.buttonArea}>
                <TouchableOpacity 
                    style={styles.button}
                    onPress={async () => {
                        await buy(() => navigation.navigate('Tickets'));
                    }}
                >
                    <Text style={styles.buttonText}>Comprar ingresso</Text>
                </TouchableOpacity>
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
        flex: 7
    },
    imageContainer: {
        top: '2%'
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
        top: 20
    },
    title: {
        fontSize: 28,
        fontWeight: 'bold',
        marginLeft: '6%',
        top: 20
    },
    location: {
        fontSize: 20,
        marginLeft: '6%',
        top: 19
    },
    description: {
        fontSize: 20,
        top: 50,
        marginLeft: 15,
        marginRight: 15
    },
    buttonArea: {
        flex: 1,
    },
    button: {
        backgroundColor: '#7ED957',
        borderRadius: 15,
        alignSelf: 'center',
        height: 50,
        width: 250,
        bottom: 0,
        alignItems: 'center',
        justifyContent: 'center'
    },
    buttonText: {
        fontSize: 18,
        color: '#fff',
        fontWeight: 'bold'
    },
});